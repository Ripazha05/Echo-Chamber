using UnityEngine;
using TMPro;

public class ComputerMinigame : MonoBehaviour
{
    [Header("UI Panels & GameObjects")]
    public GameObject panelUIKomputer;         // Objek Panel_UI_Komputer
    public GameObject tombolSimpanKeFlashdisk;   // Objek Btn_Ambil_Bukti (Tombol Salin)
    public TextMeshProUGUI teksIsiLayar;        // Objek Teks_Isi_Layar

    [Header("State Storyboard (Trigger Flag)")]
    public bool isPuzzleSolved = false;          // Flag utama Day 2 untuk progresi cerita

    // Fungsi otomatis berjalan setiap kali UI Komputer diaktifkan/dibuka
    void OnEnable()
    {
        // Tampilan petunjuk awal di sebelah kanan saat monitor menyala
        teksIsiLayar.text = "SISTEM NET-MAIL KAMPUS\n\n<b>[Status]:</b> Flashdisk Day 1 Terhubung.\nSilakan pilih inbox email di panel kiri untuk menganalisis pesan...";

        if (tombolSimpanKeFlashdisk != null)
            tombolSimpanKeFlashdisk.SetActive(false); // Sembunyikan tombol Salin di awal
    }

    // 1. Email Akademik
    public void KlikEmailBiasa1()
    {
        teksIsiLayar.text = "<b>Dari:</b> akademik@pcr.ac.id\n" +
                            "<b>Subjek:</b> [AKADEMIK] Pemberitahuan Jadwal Pengganti Kuliah\n\n" +
                            "Diberitahukan kepada seluruh mahasiswa Teknik Informatika, bahwa perkuliahan Jaringan Komputer hari ini dialihkan menjadi pengerjaan tugas mandiri melalui portal SIAK Kampus.";

        // Sembunyikan tombol salin karena email ini tidak berisi bukti
        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);
    }

    // 2. Email Tugas
    public void KlikEmailBiasa2()
    {
        teksIsiLayar.text = "<b>Dari:</b> gita_bisa@gmail.com\n" +
                            "<b>Subjek:</b> Re: Bahan Materi Presentasi Kelompok 10\n\n" +
                            "Zis, ini aku sudah rapihin file bab 1 dan bab 2 buat projek besok ya. Tolong dicek lagi bagian integrasi database-nya sebelum kita presentasi di depan dosen wali.";

        // Sembunyikan tombol salin karena email ini tidak berisi bukti
        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);
    }

    // 3. Email Bukti (Rahasia)
    public void KlikEmailBuktiRahasia()
    {
        // Emoji dihapus untuk mencegah error Unicode mask font di Unity
        teksIsiLayar.text = "<b>Dari:</b> system_alert@echo-app.net\n" +
                            "<b>Subjek:</b> [PERINGATAN] Akses Server Aplikasi Echo Ilegal\n\n" +
                            "<b>SAKSI KUNCI:</b> Terdeteksi aktivitas unggah massal dari IP Address 192.168.43.10 menggunakan akun admin bayangan. " +
                            "Data biner menunjukkan manipulasi foto korban atas nama 'Gita' berhasil ditembus pada jam 01.30 AM.\n\n" +
                            "<b>PENTING:</b> Segera unduh log biner ini ke penyimpanan eksternal untuk dijadikan barang bukti fisik!";

        // Tampilkan tombol Salin karena email ini adalah barang bukti kunci
        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(true);
    }

    // 4. Tombol Salin (Ambil Bukti)
    public void KlikSimpanKeFlashdisk()
    {
        isPuzzleSolved = true; // Trigger flag berubah jadi TRUE untuk lanjut ke interogasi Roy

        teksIsiLayar.text = "<b>PROSES UNDUH BUKTI DIGITAL...</b>\n" +
                            "--------------------------------------------------\n" +
                            "<b>[SUKSES]:</b> File 'Bukti_Log_Echo.txt' berhasil disalin ke Flashdisk Rian!\n\n" +
                            "Analisis forensik selesai. Kamu bisa menutup monitor (X) dan melakukan interogasi.";

        // Sembunyikan kembali tombol Salin karena file sudah sukses diunduh
        if (tombolSimpanKeFlashdisk != null) tombolSimpanKeFlashdisk.SetActive(false);

        Debug.Log("Sistem: Bukti diamankan! isPuzzleSolved = " + isPuzzleSolved);
    }

    // 5. Tombol Keluar (X)
    public void KlikTutupKomputer()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(false); // Sembunyikan seluruh UI monitor komputer

        // Kembalikan kursor ke mode gameplay normal
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}