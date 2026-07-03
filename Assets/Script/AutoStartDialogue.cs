using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Wajib untuk pindah scene

public class AutoStartDialogue : MonoBehaviour
{
    public GameObject panelDialog; 
    public TMP_Text komponenTeksDialog; 

    [Header("Trigger Settings")]
    public bool jalanOtomatisDiAwal = false; 

    [Header("Scene Transition Settings")]
    public bool pindahSceneSetelahSelesai = false; // Centang ini khusus di dialog terakhir Gita
    public string namaSceneTujuan; // Ketik nama scene besok (misal: Day 2)
    public GameObject panelFade; // Tarik objek UI Fade kamu ke sini

    [TextArea(2, 5)] 
    public string[] daftarDialog; 

    private int indexSekarang = 0; 
    private bool sedangAktifMembaca = false; 

    void Start()
    {
        if (jalanOtomatisDiAwal)
        {
            MulaiDialog();
        }
        
        // Pastikan panel fade mati di awal
        if (panelFade != null) panelFade.SetActive(false);
    }

    public void MulaiDialog()
    {
        if (panelDialog != null && daftarDialog.Length > 0 && komponenTeksDialog != null)
        {
            indexSekarang = 0;
            sedangAktifMembaca = true;
            komponenTeksDialog.text = daftarDialog[indexSekarang]; 
            panelDialog.SetActive(true); 
        }
    }

    void Update()
    {
        if (sedangAktifMembaca && panelDialog != null && panelDialog.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                LanjutAtauSelesaiDialog();
            }
        }
    }

    void LanjutAtauSelesaiDialog()
    {
        indexSekarang++; 

        if (indexSekarang < daftarDialog.Length)
        {
            komponenTeksDialog.text = daftarDialog[indexSekarang];
        }
        else
        {
            sedangAktifMembaca = false;
            panelDialog.SetActive(false);

            // CEK: Jika ini dialog terakhir yang harus pindah scene
            if (pindahSceneSetelahSelesai)
            {
                MulaiTransisi();
            }
        }
    }

    void MulaiTransisi()
    {
        if (panelFade != null)
        {
            panelFade.SetActive(true); // Menyalakan animasi Fade Out
            // Tunggu 2 detik (memberi waktu animasi fade selesai) baru pindah scene
            Invoke("GantiScene", 2f); 
        }
        else
        {
            GantiScene(); // Jika tidak ada panel fade, langsung pindah
        }
    }

    void GantiScene()
    {
        SceneManager.LoadScene(namaSceneTujuan);
    }
}