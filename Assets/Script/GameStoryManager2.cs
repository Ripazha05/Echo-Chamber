using UnityEngine;
using TMPro;
using System.Collections;

public class GameStoryManager2 : MonoBehaviour
{
    [Header("UI Panel Intro / Pengantar")]
    // Tempat meletakkan panel yang ingin dimunculkan paling pertama
    public GameObject panelIntro;

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

    void Start()
    {
        // Setup text quest di awal
        if (questTitleText != null) questTitleText.text = judulQuestAwal;
        if (questDescriptionText != null) questDescriptionText.text = deskripsiQuestAwal;

        // KONDISI AWAL UI
        if (panelIntro != null) panelIntro.SetActive(true);   // Panel Intro AKTIF duluan
        if (panelDialog != null) panelDialog.SetActive(false); // Panel Dialog SEMBUNYI dulu
        if (panelQuest != null) panelQuest.SetActive(false);   // Panel Quest SEMBUNYI dulu
    }

    void Update()
    {
        // Deteksi klik mouse kiri atau Spasi
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // JIKA panel intro masih aktif, klik akan menutup intro dan membuka dialog
            if (panelIntro != null && panelIntro.activeSelf)
            {
                SelesaiIntroDanMulaiDialog();
            }
            // JIKA panel intro sudah tutup dan dialog sedang aktif, lanjut dialog seperti biasa
            else if (panelDialog != null && panelDialog.activeSelf)
            {
                LanjutDialog();
            }
        }
    }

    // Fungsi khusus untuk berpindah dari Intro ke Dialog
    public void SelesaiIntroDanMulaiDialog()
    {
        if (panelIntro != null) panelIntro.SetActive(false);
        if (panelDialog != null) panelDialog.SetActive(true);

        // Langsung tampilkan kalimat pertama saat dialog terbuka
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
            // Jika masih ada dialog, tampilkan kalimat berikutnya
            TampilkanKalimat();
        }
        else
        {
            // Selesai semua dialog -> Matikan dialog dan munculkan Quest
            if (panelDialog != null) panelDialog.SetActive(false);
            if (panelQuest != null) panelQuest.SetActive(true);
        }
    }
}