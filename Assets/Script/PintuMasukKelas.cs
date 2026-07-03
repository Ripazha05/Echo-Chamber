using UnityEngine;
using UnityEngine.SceneManagement; // Wajib untuk memindahkan scene

public class PintuMasukKelas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Memastikan hanya objek dengan Tag "Player" yang bisa memicu
        if (other.CompareTag("Player"))
        {
            // Mengambil nomor index scene aktif saat ini (Day 2)
            int indexSceneSekarang = SceneManager.GetActiveScene().buildIndex;

            // Membuka scene berikutnya di Build Settings (Day 2 + 1 = Interior_Kelas_Baru)
            SceneManager.LoadScene(indexSceneSekarang + 1);
        }
    }
}