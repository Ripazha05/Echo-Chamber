using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransitionDialog : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI nameTagText;

    [Header("Settings")]
    [SerializeField] private string nextSceneName = "Day 3";
    [SerializeField] private string message = "Semua bukti sudah terkumpul!\nSaatnya aku buktikan kebenaran ini...";
    [SerializeField] private string characterName = "Rian";

    private bool isDialogActive = false;

    void Start()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    void Update()
    {
        // Klik layar = lanjut ke scene berikutnya
        if (isDialogActive && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void ShowTransitionDialog()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        if (dialogText != null)
            dialogText.text = message;

        if (nameTagText != null)
            nameTagText.text = characterName;

        isDialogActive = true;

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}