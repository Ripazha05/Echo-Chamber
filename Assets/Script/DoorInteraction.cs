    using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string sceneToLoad = "DayScene_Indoor";
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI Prompt")]
    [SerializeField] private GameObject promptUI; // Panel "Tekan E untuk masuk"

    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        // Cari player otomatis lewat tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Pastikan prompt disembunyikan dulu
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            // Player sudah dekat pintu
            if (!playerInRange)
            {
                playerInRange = true;
                if (promptUI != null) promptUI.SetActive(true);
            }

            // Cek input E
            if (Input.GetKeyDown(interactKey))
            {
                MasukKampus();
            }
        }
        else
        {
            // Player menjauh
            if (playerInRange)
            {
                playerInRange = false;
                if (promptUI != null) promptUI.SetActive(false);
            }
        }
    }

    void MasukKampus()
    {
        // Update quest sebelum pindah scene
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.SetQuest(
                "Hari 1: Insiden",
                "Kamu masuk ke dalam kampus.\nCari mading digital di koridor!"
            );
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    // Visualisasi range di Scene view (opsional, buat debugging)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}