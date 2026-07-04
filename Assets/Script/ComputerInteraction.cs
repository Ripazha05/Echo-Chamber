using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    [Header("UI Komputer")]
    public GameObject panelUIKomputer; // Tempat menaruh Panel_UI_Komputer dari Canvas

    private bool playerDekatMeja = false;

    void Start()
    {
        // Pastikan UI monitor mati saat game baru dimulai
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(false);
    }

    void Update()
    {
        // Jika player dekat meja dan menekan tombol E
        if (playerDekatMeja && Input.GetKeyDown(KeyCode.E))
        {
            // TAMBAHKAN BARIS INI UNTUK TES INPUT
            Debug.Log("Sistem: Tombol E terdeteksi ditekan oleh Player!");

            if (panelUIKomputer != null && !panelUIKomputer.activeSelf)
            {
                BukaLayarKomputer();
            }
        }
    }

    void BukaLayarKomputer()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(true);

        // MEMUNCULKAN KURSOR MOUSE (Penting agar player bisa nge-klik email)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Fungsi untuk menutup komputer (bisa dipanggil dari tombol X)
    public void TutupLayarKomputer()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(false);

        // KUNCI KURSOR KEMBALI (Untuk gameplay Third-Person normal)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Deteksi saat Player masuk ke area kotak hijau meja
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDekatMeja = true;
            Debug.Log("Player dekat komputer. Tekan E untuk membuka.");
        }
    }

    // Deteksi saat Player menjauh dari meja
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDekatMeja = false;
            TutupLayarKomputer(); // Otomatis tutup layar kalau player pergi menjauh
        }
    }
}
