using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk manajemen scene

public class PindahRuangan : MonoBehaviour
{
    [Header("Pengaturan Scene")]
    [Tooltip("Masukkan nama scene tujuan persis seperti di folder Scenes")]
    public string namaSceneTujuan;

    // Fungsi bawaan Unity yang otomatis jalan saat objek dengan Collider/Rigidbody masuk ke area Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek yang menabrak pintu memiliki Tag "Player"
        if (other.CompareTag("Player"))
        {
            // Pindah ke scene kelas
            SceneManager.LoadScene(namaSceneTujuan);
        }
    }
}