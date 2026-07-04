using UnityEngine;
using TMPro;
using UnityEngine.Playables; // Wajib untuk mengontrol Timeline

public class EndingCutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct DataDialog
    {
        public string namaKarakter;
        [TextArea(2, 4)] public string kalimatDialog;
    }

    [Header("UI Dialog Settings")]
    public GameObject panelDialog;
    public TextMeshProUGUI komponenTeksDialog;
    public TextMeshProUGUI komponenTeksNama;

    [Header("Daftar Kalimat Ending")]
    public DataDialog[] daftarDialogEnding;
    
    [Header("Timeline Reference")]
    public PlayableDirector timelineDirector; // Tarik objek CutsceneManager kamu ke sini

    private int indeksDialog = 0;
    private int indeksMaksimalUntukSesiIni = 0;
    private bool sedangAktifMembaca = false;

    void Start()
    {
        // Pastikan di awal scene, panel dialog tersembunyi sampai dipanggil Timeline
        if (panelDialog != null) panelDialog.SetActive(false);
    }

    void Update()
    {
        // Deteksi klik player untuk lanjut teks saat Timeline sedang di-pause
        if (sedangAktifMembaca && panelDialog != null && panelDialog.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                LanjutDialogEnding();
            }
        }
    }

    // FUNGSI UTAMA: Dipanggil untuk memutar beberapa baris dialog
    // Contoh: PutarSesiDialog(0, 2) artinya mainkan baris 0 sampai baris 2, lalu jalanin lagi kameranya
    public void PutarSesiDialog(int indexMulai, int indexSelesai)
    {
        if (timelineDirector != null)
        {
            timelineDirector.Pause(); // Rem/Pause kameranya di frame ini
        }

        indeksDialog = indexMulai;
        indeksMaksimalUntukSesiIni = indexSelesai;
        sedangAktifMembaca = true;

        if (panelDialog != null) panelDialog.SetActive(true);
        TampilkanKalimat();
    }

    // ====== WRAPPER FUNCTIONS (tanpa parameter) ======
    // Signal Receiver di Unity cuma bisa manggil fungsi dengan 0 atau 1 parameter,
    // jadi fungsi PutarSesiDialog(int, int) di atas tidak akan muncul di dropdown Signal.
    // Makanya dibungkus jadi fungsi-fungsi khusus per sesi seperti di bawah ini.
    // Tinggal tambah MulaiDialogSesi4(), Sesi5(), dst kalau perlu lebih banyak.

    public void MulaiDialogSesi1()
    {
        PutarSesiDialog(0, 2); // Tampilkan dialog index 0 (adsad)
    }

    public void MulaiDialogSesi2()
    {
        PutarSesiDialog(3, 5); // Tampilkan dialog index 1 (wqeqwe)
    }

    public void MulaiDialogSesi3()
    {
        PutarSesiDialog(6, 8); // Tampilkan dialog index 2 (asdad)
    }
    public void MulaiDialogSesi4()
    {
        PutarSesiDialog(9, 9); // Tampilkan dialog index 2 (asdad)
    }
     public void MulaiDialogSesi5()
    {
        PutarSesiDialog(10, 11); // Tampilkan dialog index 2 (asdad)
    }
     public void MulaiDialogSesi6()
    {
        PutarSesiDialog(12, 12); // Tampilkan dialog index 2 (asdad)
    }
     public void MulaiDialogSesi7()
    {
        PutarSesiDialog(13, 13); // Tampilkan dialog index 2 (asdad)
    }
     public void MulaiDialogSesi8()
    {
        PutarSesiDialog(14, 16); // Tampilkan dialog index 2 (asdad)
    }
     public void MulaiDialogSesi9()
    {
        PutarSesiDialog(17, 17); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi10()
    {
        PutarSesiDialog(18, 18); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi11()
    {
        PutarSesiDialog(19, 19); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi12()
    {
        PutarSesiDialog(20, 20); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi13()
    {
        PutarSesiDialog(21, 21); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi14()
    {
        PutarSesiDialog(22, 23); // Tampilkan dialog index 2 (asdad)
    }
         public void MulaiDialogSesi15()
    {
        PutarSesiDialog(24, 25); // Tampilkan dialog index 2 (asdad)
    }
             public void MulaiDialogSesi16()
    {
        PutarSesiDialog(26, 26); // Tampilkan dialog index 2 (asdad)
    }

    // ====== END WRAPPER FUNCTIONS ======

    void TampilkanKalimat()
    {
        if (daftarDialogEnding.Length > 0 && indeksDialog <= indeksMaksimalUntukSesiIni && indeksDialog < daftarDialogEnding.Length)
        {
            komponenTeksDialog.text = daftarDialogEnding[indeksDialog].kalimatDialog;
            
            // Atur kotak nama pembicara
            if (komponenTeksNama != null)
            {
                string nama = daftarDialogEnding[indeksDialog].namaKarakter;
                if (!string.IsNullOrEmpty(nama))
                {
                    komponenTeksNama.text = nama;
                }
                else
                {
                    komponenTeksNama.text = ""; // Kosongkan jika narasi batin
                }
            }
        }
    }

    void LanjutDialogEnding()
    {
        indeksDialog++;

        // Cek apakah masih ada dialog di sesi cutscene ini
        if (indeksDialog <= indeksMaksimalUntukSesiIni && indeksDialog < daftarDialogEnding.Length)
        {
            TampilkanKalimat();
        }
        else
        {
            // Jika batas baris dialog sesi ini sudah habis, tutup panel dan jalankan lagi Timeline-nya
            sedangAktifMembaca = false;
            if (panelDialog != null) panelDialog.SetActive(false);

            if (timelineDirector != null)
            {
                timelineDirector.Play(); // Resume kamera ke shot selanjutnya!
            }
        }
    }
}