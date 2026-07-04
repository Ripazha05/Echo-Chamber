using UnityEngine;
using TMPro;

public class ComputerMinigame : MonoBehaviour
{
    [Header("UI Panels & GameObjects")]
    public GameObject panelUIKomputer;
    public GameObject tombolSimpanKeFlashdisk;
    public TextMeshProUGUI teksIsiLayar;

    [Header("Transition Dialog")]
    public GameObject transitionDialogPanel; // drag TransitionDialogPanel ke sini
    public SceneTransitionDialog sceneTransition; // drag SceneTransition object ke sini

    [Header("State Storyboard (Trigger Flag)")]
    public bool isPuzzleSolved = false;

    void OnEnable()
    {
        teksIsiLayar.text = "SISTEM NET-MAIL KAMPUS\n\n<b>[Status]:</b> Flashdisk Day 1 Terhubung.\nSilakan pilih inbox email di panel kiri untuk menganalisis pesan...";

        if (tombolSimpanKeFlashdisk != null)
            tombolSimpanKeFlashdisk.SetActive(false);
    }

    public void KlikEmailBiasa1()
    {
        teksIsiLayar.text = "<b>Dari:</b> akademik@pcr.ac.id\n" +
                            "<b>Subjek:</b> [AKADEMIK] Pemberitahuan Jadwal Pengganti Kuliah\n\n" +
                            "Diberitahukan kepada seluruh mahasiswa Teknik Informatika, bahwa perkuliahan Jaringan Komputer hari ini dialihkan menjadi pengerjaan tugas mandiri melalui portal SIAK Kampus.";

        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);
    }

    public void KlikEmailBiasa2()
    {
        teksIsiLayar.text = "<b>Dari:</b> gita_bisa@gmail.com\n" +
                            "<b>Subjek:</b> Re: Bahan Materi Presentasi Kelompok 10\n\n" +
                            "Zis, ini aku sudah rapihin file bab 1 dan bab 2 buat projek besok ya. Tolong dicek lagi bagian integrasi database-nya sebelum kita presentasi di depan dosen wali.";

        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);
    }

    public void KlikEmailBuktiRahasia()
    {
        teksIsiLayar.text = "<b>Dari:</b> system_alert@echo-app.net\n" +
                            "<b>Subjek:</b> [PERINGATAN] Akses Server Aplikasi Echo Ilegal\n\n" +
                            "<b>SAKSI KUNCI:</b> Terdeteksi aktivitas unggah massal dari IP Address 192.168.43.10 menggunakan akun admin bayangan. " +
                            "Data biner menunjukkan manipulasi foto korban atas nama 'Gita' berhasil ditembus pada jam 01.30 AM.\n\n" +
                            "<b>PENTING:</b> Segera unduh log biner ini ke penyimpanan eksternal untuk dijadikan barang bukti fisik!";

        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(true);
    }

    public void KlikSimpanKeFlashdisk()
    {
        isPuzzleSolved = true;

        teksIsiLayar.text = "<b>PROSES UNDUH BUKTI DIGITAL...</b>\n" +
                            "--------------------------------------------------\n" +
                            "<b>[SUKSES]:</b> File 'Bukti_Log_Echo.txt' berhasil disalin ke Flashdisk Riyu!\n\n" +
                            "Analisis forensik selesai. Kamu bisa menutup monitor (X) dan melakukan interogasi.";

        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);

        Debug.Log("Sistem: Bukti diamankan! isPuzzleSolved = " + isPuzzleSolved);
    }

    public void KlikTutupKomputer()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(false);

        // Kalau puzzle sudah selesai, tampilkan dialog transisi ke Day 3
        if (isPuzzleSolved)
        {
            if (sceneTransition != null)
                sceneTransition.ShowTransitionDialog();
        }
        else
        {
            // Kalau belum selesai, kembalikan ke gameplay normal
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}