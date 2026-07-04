using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    private Transform player;
    private CharacterController playerController;

    [Header("Settings Teleport")]
    public Transform targetDestination; // Titik tujuan setelah teleport
    public float interactionDistance = 3f; // Jarak interaksi dengan pintu
    public KeyCode interactKey = KeyCode.E; // Tombol interaksi

    void Start()
    {
        // Mencari objek player secara otomatis lewat Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerController = playerObj.GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Hitung jarak matematika antara player dan pintu
        float distance = Vector3.Distance(transform.position, player.position);

        // Jika player dekat dan menekan tombol E
        if (distance <= interactionDistance && Input.GetKeyDown(interactKey))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        if (targetDestination == null)
        {
            Debug.LogError("Target Destination belum dimasukkan di Inspector pintu!");
            return;
        }

        // PENTING: Matikan Character Controller sebelum memindahkan posisi
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Pindahkan posisi dan rotasi player ke titik target
        player.position = targetDestination.position;
        player.rotation = targetDestination.rotation;

        // Hidupkan kembali Character Controller setelah posisi berubah
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        Debug.Log("Player berhasil di-teleport ke: " + targetDestination.name);
    }
}
