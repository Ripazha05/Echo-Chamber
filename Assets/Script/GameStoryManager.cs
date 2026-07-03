using UnityEngine;
using TMPro;
using System.Collections;

public class GameStoryManager : MonoBehaviour
{
    [Header("UI Dialog Bawah")]
    public GameObject panelDialog;
    public TextMeshProUGUI teksDialog;
    
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
        }
    }

    IEnumerator MainkanSuaraDanCutsceneHP()
    {
        // 1. Matikan dialog bawah sebentar biar fokus sinematik
        panelDialog.SetActive(false);

        // 2. Bunyikan suara ting-ting 2x
        if (audioSource != null && suaraNotifikasi != null)
        {
            audioSource.PlayOneShot(suaraNotifikasi);
            yield return new WaitForSeconds(0.4f);
            audioSource.PlayOneShot(suaraNotifikasi);
        }

        // 3. Pindah kamera ke First Person HP
        if (kameraGameplay != null) kameraGameplay.SetActive(false);
        if (kameraFPSHP != null) kameraFPSHP.SetActive(true);

        // 4. Pemicu Animasi HP Naik dari bawah layar
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

        // Kembalikan ke kamera Third Person normal
        if (kameraFPSHP != null) kameraFPSHP.SetActive(false);
        if (kameraGameplay != null) kameraGameplay.SetActive(true);

        // Munculkan kembali dialog dan update teksnya
        panelDialog.SetActive(true);
        TampilkanKalimat(); 
    }
}