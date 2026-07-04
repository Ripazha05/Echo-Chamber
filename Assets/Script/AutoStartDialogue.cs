using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoStartDialogue : MonoBehaviour
{
    // Membuat struktur data agar setiap kalimat punya nama pembicara sendiri
    [System.Serializable]
    public struct DataDialog
    {
        public string namaKarakter; // Nama yang akan muncul di kotak nama
        [TextArea(2, 4)] public string kalimatDialog; // Isi omongannya
    }

    [Header("UI Dialog Settings")]
    public GameObject panelDialog;
    public TextMeshProUGUI komponenTeksDialog;

    [Header("UI Name Tag Settings")]
    public TextMeshProUGUI komponenTeksNama; // Seret teks UI komponen Nama ke sini

    [Header("Daftar Kalimat Dialog (Baru)")]
    public DataDialog[] daftarDialogSistemBaru; 
    private int indeksDialog = 0;

    [Header("Sistem Quest Baru (Selesai Dialog)")]
    public GameObject panelQuest;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    
    [Space(5)]
    public string judulQuestBaru;
    [TextArea(2, 3)]
    public string deskripsiQuestBaru;

    [Header("Audio Efek Notifikasi Quest")]
    public AudioSource audioSource;
    public AudioClip suaraNotifikasi;

    [Header("Pengaturan Transisi Scene (Hari Esok)")]
    public bool jalanOtomatisDiAwal = false;
    public bool pindahSceneSetelahSelesai = false;
    public string namaSceneTujuan;
    public GameObject panelFade;
    [Header("Sistem Cutscene Timeline")]
public UnityEngine.Playables.PlayableDirector timelineDirector; // Tarik objek CutsceneManager_Day3 ke sini

    private bool sedangAktifMembaca = false;

    void Start()
    {
        if (jalanOtomatisDiAwal && panelQuest != null) panelQuest.SetActive(false);
        if (panelFade != null) panelFade.SetActive(false);

        if (jalanOtomatisDiAwal)
        {
            MulaiDialog();
        }
    }

    public void MulaiDialog()
    {
        if (panelDialog != null && daftarDialogSistemBaru.Length > 0 && komponenTeksDialog != null)
        {
            indeksDialog = 0;
            sedangAktifMembaca = true;
            panelDialog.SetActive(true);
            TampilkanKalimat();
        }
    }

    public void MulaiDialogOtomatis()
    {
        MulaiDialog();
    }

    void Update()
    {
        if (sedangAktifMembaca && panelDialog != null && panelDialog.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                LanjutDialog();
            }
        }
    }

    void TampilkanKalimat()
    {
        if (daftarDialogSistemBaru.Length > 0 && indeksDialog < daftarDialogSistemBaru.Length)
        {
            // Set Kalimat Dialog
            komponenTeksDialog.text = daftarDialogSistemBaru[indeksDialog].kalimatDialog;
            
            // Set Nama Pembicara secara dinamis tiap baris teks!
            if (komponenTeksNama != null)
            {
                string nama = daftarDialogSistemBaru[indeksDialog].namaKarakter;
                if (!string.IsNullOrEmpty(nama))
                {
                    komponenTeksNama.gameObject.SetActive(true);
                    komponenTeksNama.text = nama;
                }
                else
                {
                    // Jika dikosongkan (untuk suara batin/narasi anonim), hilangkan kotak namanya
                    komponenTeksNama.text = "";
                }
            }
        }
    }

    void LanjutDialog()
{
    indeksDialog++;

    if (indeksDialog < daftarDialogSistemBaru.Length)
    {
        TampilkanKalimat();
    }
    else
    {
        sedangAktifMembaca = false;
        panelDialog.SetActive(false);

        MunculkanQuestBaruCampus();

        // FITUR BARU: Jika sedang di dalam cutscene Timeline, jalankan lagi Timeline-nya!
        if (timelineDirector != null)
        {
            timelineDirector.Play(); // Resume Timeline ke angle kamera berikutnya
        }

        if (pindahSceneSetelahSelesai)
        {
            MulaiTransisiKeSceneLain();
        }
    }
}

    void MunculkanQuestBaruCampus()
    {
        if (panelQuest != null)
        {
            if (questTitleText != null && !string.IsNullOrEmpty(judulQuestBaru)) questTitleText.text = judulQuestBaru;
            if (questDescriptionText != null && !string.IsNullOrEmpty(deskripsiQuestBaru)) questDescriptionText.text = deskripsiQuestBaru;

            panelQuest.SetActive(true);

            if (audioSource != null && suaraNotifikasi != null)
            {
                audioSource.PlayOneShot(suaraNotifikasi);
            }
        }
    }

    void MulaiTransisiKeSceneLain()
    {
        if (panelFade != null)
        {
            panelFade.SetActive(true);
            Invoke("GantiScene", 2f);
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
    // FITUR CUTSCENE: Memaksa dialog memunculkan index kalimat tertentu dari Timeline
public void TriggerDialogSpesifik(int indexKalimat)
{
    if (panelDialog != null && daftarDialogSistemBaru.Length > indexKalimat)
    {
        sedangAktifMembaca = false; // Matikan input klik player agar timeline yang kendalikan
        panelDialog.SetActive(true);
        
        // Pasang teks kalimat dan nama karakter sesuai index yang diminta
        komponenTeksDialog.text = daftarDialogSistemBaru[indexKalimat].kalimatDialog;
        if (komponenTeksNama != null)
        {
            komponenTeksNama.text = daftarDialogSistemBaru[indexKalimat].namaKarakter;
        }
    }
}

public void TutupPanelDialogCutscene()
{
    if (panelDialog != null) panelDialog.SetActive(false);
}
// Fungsi untuk memotong alur Timeline agar player bisa klik-klik dialog dulu
public void PauseTimelineUntukDialog(int indexMulai)
{
    if (timelineDirector != null)
    {
        timelineDirector.Pause(); // Menghentikan pergerakan kamera sementara
    }
    
    // Jalankan dialog dari index yang ditentukan
    if (panelDialog != null && daftarDialogSistemBaru.Length > indexMulai)
    {
        indeksDialog = indexMulai;
        sedangAktifMembaca = true;
        panelDialog.SetActive(true);
        TampilkanKalimat();
    }
}
}