using System.Collections;
using UnityEngine;
using TMPro; // Hapus atau ganti jika menggunakan UI Text biasa

public class IntroCutscene : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dayTextObject; // Tarik objek Day2Text ke sini
    public GameObject backgroundObject; // BARU: Tarik objek BackgroundHitam ke sini

    [Header("Player Settings")]
    public GameObject playerObject; // Tarik objek ManUpdate2 ke sini
    public float delayDuration = 3f; // Durasi teks muncul (3 detik)

    private PlayerMovement playerMovementScript;
    private CharacterController characterController;

    void Start()
    {
        // Ambil komponen pergerakan dari player
        if (playerObject != null)
        {
            playerMovementScript = playerObject.GetComponent<PlayerMovement>();
            characterController = playerObject.GetComponent<CharacterController>();
        }

        // Jalankan proses cutscene sejenak
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // 1. Matikan kontrol player saat awal mulai
        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (characterController != null) characterController.enabled = false;

        // 2. Pastikan teks "Day 2" dan Background Hitam aktif saat mulai
        if (dayTextObject != null) dayTextObject.SetActive(true);
        if (backgroundObject != null) backgroundObject.SetActive(true); // BARU

        // 3. Tunggu selama durasi yang ditentukan (misal 3 detik)
        yield return new WaitForSeconds(delayDuration);

        // 4. Sembunyikan teks dan background hitam setelah beres menunggu
        if (dayTextObject != null) dayTextObject.SetActive(false);
        if (backgroundObject != null) backgroundObject.SetActive(false); // BARU

        // 5. Aktifkan kembali kontrol agar player bisa bergerak
        if (playerMovementScript != null) playerMovementScript.enabled = true;
        if (characterController != null) characterController.enabled = true;
    }
}