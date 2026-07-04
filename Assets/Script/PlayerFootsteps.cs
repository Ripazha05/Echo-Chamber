using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips; // Kamu bisa masukkan lebih dari 1 variasi suara langkah biar tidak bosan

    // Fungsi ini yang akan dipanggil oleh animasi
    public void PlayFootstepSound()
    {
        if (audioSource != null && footstepClips.Length > 0)
        {
            // Pilih suara langkah secara acak dari daftar agar terdengar natural
            int randomIndex = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[randomIndex]);
        }
    }
}