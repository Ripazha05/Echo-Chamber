using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI Reference")]
    // Tarik objek 'GitaDialogueManager' ke kolom ini di Inspector Gita
    public AutoStartDialogue scriptDialogGita; 
    [SerializeField] private GameObject promptUI; 

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

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                if (promptUI != null) promptUI.SetActive(true);
            }

            // Jika tekan E dan panel dialog sedang mati, panggil dialog Gita
            if (Input.GetKeyDown(interactKey) && scriptDialogGita != null && !scriptDialogGita.panelDialog.activeSelf)
            {
                if (promptUI != null) promptUI.SetActive(false);
                
                // Memanggil fungsi MulaiDialog() khusus milik Gita
                scriptDialogGita.MulaiDialog(); 
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                if (promptUI != null) promptUI.SetActive(false);
                if (scriptDialogGita != null && scriptDialogGita.panelDialog != null) 
                    scriptDialogGita.panelDialog.SetActive(false);
            }
        }
    }
}