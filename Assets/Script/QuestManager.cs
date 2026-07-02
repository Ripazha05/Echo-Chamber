using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("UI Quest")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI questTitle;

    // Flag Day 1
    [HideInInspector] public bool isPostRead = false;
    [HideInInspector] public bool isGitaInterrogated = false;
    [HideInInspector] public bool hasEvidenceDay1 = false;

    void Awake()
    {
        // Singleton biar bisa diakses dari script manapun
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Quest pertama langsung muncul saat scene Day 1 dibuka
        SetQuest("Hari 1: Insiden", "Kamu mendengar ada kehebohan di kampus.\nPergi ke kampus dan cari tahu apa yang terjadi!");
    }

    public void SetQuest(string title, string description)
    {
        if (questPanel != null) questPanel.SetActive(true);
        if (questTitle != null) questTitle.text = title;
        if (questText != null) questText.text = description;
    }

    public void HideQuest()
    {
        if (questPanel != null) questPanel.SetActive(false);
    }

    // Cek apakah Day 1 sudah selesai semua
    public bool IsDay1Complete()
    {
        return isPostRead && isGitaInterrogated && hasEvidenceDay1;
    }
}