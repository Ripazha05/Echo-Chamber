using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // WAJIB: Untuk memicu perpindahan scene

public class GameStoryManager : MonoBehaviour
{
    [Header("UI Dialog Bawah")]
    public GameObject panelDialog;
    public TextMeshProUGUI teksDialog;
    
    // VARIABEL BARU: Untuk menampilkan Name Tag karakter di atas panel dialog
    [Header("UI Name Tag Settings")]
    public TextMeshProUGUI komponenTeksNama; 
    public string namaPembicara; 

    [Header("Isi Dialog")]
    [TextArea(2, 5)]
    public string[] daftarDialog;
    private int indeksDialog = 0;

    [Header("UI Quest Kiri Atas")]
    public GameObject panelQuest;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;

    [Header("Isi Data Quest")]
    public string judulQuestAwal = "Hari 1: Insiden";
    [TextArea(2, 3)]
    public string deskripsiQuestAwal = "Kamu mendengar ada kehebohan di kampus.\nPergi ke kampus dan cari tahu apa yang terjadi!";

    [Header("Audio Efek")]
    public AudioSource audioSource;
    public AudioClip suaraNotifikasi;
    private bool sudahBunyiNotif = false;

    [Header("Sistem Kamera & Cutscene")]
    public GameObject kameraOpeningCutscene;  // Kamera dari depan ke belakang
    public GameObject kameraGameplay;         // Main Camera asli kamu
    public GameObject kameraFPSHP;            // Kamera khusus lihat HP
    public float durasiKameraOpening = 3.0f; 
    
    private bool cutsceneOpeningSelesai = false; 
    private bool sedangLihatHP = false;

    // VARIABEL BARU: Pengaturan transisi otomatis ke hari esok (jika dibutuhkan)
    [Header("Next Day Transition")]
    public bool pindahSceneSetelahSelesai = false; 
    public string namaSceneTujuan; 
    public GameObject panelFade;

    void Start()
    {
        // Setup text quest di awal (masih tersembunyi)
        if (questTitleText != null) questTitleText.text = judulQuestAwal;
        if (questDescriptionText != null) questDescriptionText.text = deskripsiQuestAwal;

        // Kondisi awal UI
        panelDialog.SetActive(false);
        panelQuest.SetActive(false);

        // Kondisi awal Kamera (Hanya kamera opening yang aktif)
        if (kameraOpeningCutscene != null) kameraOpeningCutscene.SetActive(true);
        if (kameraGameplay != null) kameraGameplay.SetActive(false);
        if (kameraFPSHP != null) kameraFPSHP.SetActive(false);

        if (panelFade != null) panelFade.SetActive(false);

        // Mulai hitung mundur nunggu kamera opening selesai bergerak
        StartCoroutine(TungguOpeningCutscene());
    }

    void Update()
    {
        // Deteksi klik mouse kiri atau Spasi
        if (cutsceneOpeningSelesai && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (sedangLihatHP)
            {
                KembaliKeThirdPerson();
            }
            else
            {
                LanjutDialog();
            }
        }
    }

    IEnumerator TungguOpeningCutscene()
    {
        yield return new WaitForSeconds(durasiKameraOpening);

        // Tukar ke kamera gameplay utama
        if (kameraOpeningCutscene != null) kameraOpeningCutscene.SetActive(false);
        if (kameraGameplay != null) kameraGameplay.SetActive(true);

        cutsceneOpeningSelesai = true;
        panelDialog.SetActive(true);
        TampilkanKalimat();
    }

    void TampilkanKalimat()
    {
        if (daftarDialog.Length > 0 && indeksDialog < daftarDialog.Length)
        {
            teksDialog.text = daftarDialog[indeksDialog];
            
            // PERBAIKAN: Update kotak nama setiap kali kalimat ditampilkan
            UpdateTeksNama();
        }
    }

    // FUNGSI BARU: Untuk mengatur kapan nama muncul atau kosong (jika berupa narasi)
    void UpdateTeksNama()
    {
        if (komponenTeksNama != null)
        {
            if (!string.IsNullOrEmpty(namaPembicara))
            {
                komponenTeksNama.text = namaPembicara;
            }
            else
            {
                komponenTeksNama.text = ""; // Kosongkan jika merupakan teks batin/narasi
            }
        }
    }

    void LanjutDialog()
    {
        indeksDialog++;

        if (indeksDialog < daftarDialog.Length)
        {
            // Jika masuk ke dialog kedua (indeks 1), potong alur untuk cek HP
            if (indeksDialog == 1 && !sudahBunyiNotif)
            {
                sudahBunyiNotif = true;
                StartCoroutine(MainkanSuaraDanCutsceneHP());
            }
            else
            {
                TampilkanKalimat();
            }
        }
        else
        {
            // Selesai semua dialog -> Munculkan Quest
            panelDialog.SetActive(false);
            panelQuest.SetActive(true);

            // PERBAIKAN: Jika dicentang ganti scene setelah dialog beres, panggil transisinya
            if (pindahSceneSetelahSelesai)
            {
                MulaiTransisiKeHariEsok();
            }
        }
    }

    IEnumerator MainkanSuaraDanCutsceneHP()
    {
        panelDialog.SetActive(false);

        if (audioSource != null && suaraNotifikasi != null)
        {
            audioSource.PlayOneShot(suaraNotifikasi);
            yield return new WaitForSeconds(0.4f);
            audioSource.PlayOneShot(suaraNotifikasi);
        }

        if (kameraGameplay != null) kameraGameplay.SetActive(false);
        if (kameraFPSHP != null) kameraFPSHP.SetActive(true);

        Animator fpsCamAnimator = kameraFPSHP.GetComponent<Animator>();
        if (fpsCamAnimator != null)
        {
            fpsCamAnimator.Play("HP_Naik_Anim");
        }

        sedangLihatHP = true;
    }

    void KembaliKeThirdPerson()
    {
        sedangLihatHP = false;

        if (kameraFPSHP != null) kameraFPSHP.SetActive(false);
        if (kameraGameplay != null) kameraGameplay.SetActive(true);

        panelDialog.SetActive(true);
        TampilkanKalimat(); 
    }

    // FUNGSI BARU: Mengurus animasi fade sebelum ganti scene
    void MulaiTransisiKeHariEsok()
    {
        if (panelFade != null)
        {
            panelFade.SetActive(true); 
            Invoke("GantiScene", 2f); // Beri waktu 2 detik agar efek hitam selesai render
        }
        else
        {
            GantiScene(); 
        }
    }

    void GantiScene()
    {
        SceneManager.LoadScene(namaSceneTujuan);
    }
}