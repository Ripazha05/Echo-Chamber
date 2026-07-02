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

    [Header("Kamera & Cutscene Setting")]
    public GameObject kameraCutscene;  // Masukkan Camera_Cutscene ke sini
    public GameObject kameraGameplay;  // Masukkan Main Camera ke sini
    public float durasiKameraBergerak = 3.0f; 
    private bool cutsceneSelesai = false; 

    void Start()
    {
        if (questTitleText != null) questTitleText.text = judulQuestAwal;
        if (questDescriptionText != null) questDescriptionText.text = deskripsiQuestAwal;

        panelDialog.SetActive(false);
        panelQuest.SetActive(false);

        // KONDISI AWAL: Kamera cutscene hidup, kamera gameplay mati
        if (kameraCutscene != null) kameraCutscene.SetActive(true);
        if (kameraGameplay != null) kameraGameplay.SetActive(false);

        StartCoroutine(TungguCutsceneKamera());
    }

    void Update()
    {
        if (cutsceneSelesai && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            LanjutDialog();
        }
    }

    IEnumerator TungguCutsceneKamera()
    {
        yield return new WaitForSeconds(durasiKameraBergerak);

        // CUTSCENE SELESAI: Tukar kamera! Kamera cutscene mati, kamera gameplay hidup kembali
        if (kameraCutscene != null) kameraCutscene.SetActive(false);
        if (kameraGameplay != null) kameraGameplay.SetActive(true);

        cutsceneSelesai = true;
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
            TampilkanKalimat();

            if (indeksDialog == 1 && !sudahBunyiNotif)
            {
                sudahBunyiNotif = true;
                StartCoroutine(MainkanSuaraDuaKali());
            }
        }
        else
        {
            panelDialog.SetActive(false);
            panelQuest.SetActive(true);
        }
    }

    IEnumerator MainkanSuaraDuaKali()
    {
        if (audioSource != null && suaraNotifikasi != null)
        {
            audioSource.PlayOneShot(suaraNotifikasi);
            yield return new WaitForSeconds(0.4f);
            audioSource.PlayOneShot(suaraNotifikasi);
        }
    }
}