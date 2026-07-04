using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI Reference")]
    // Hubungkan dengan objek GitaDialogueManager di Hierarchy
    public AutoStartDialogue scriptDialogGita; 
    [SerializeField] private GameObject promptUI; // Teks "Tekan E untuk bicara" (opsional)

    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        // Hitung jarak manual (identik dengan sistem pintumu)
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                if (promptUI != null) promptUI.SetActive(true);
            }

            // Jika tekan E dan panel dialog sedang tidak terbuka, panggil dialog Gita
            if (Input.GetKeyDown(interactKey) && scriptDialogGita != null && !scriptDialogGita.panelDialog.activeSelf)
            {
                if (promptUI != null) promptUI.SetActive(false);
                
                // Memanggil fungsi MulaiDialog() milik GitaDialogueManager
                scriptDialogGita.MulaiDialog(); 
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                if (promptUI != null) promptUI.SetActive(false);
                
                // Tutup paksa dialog jika pemain kabur di tengah obrolan
                if (scriptDialogGita != null && scriptDialogGita.panelDialog != null) 
                    scriptDialogGita.panelDialog.SetActive(false);
            }
        }
    }

    // Menggambar lingkaran jarak di Scene View biar gampang debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}