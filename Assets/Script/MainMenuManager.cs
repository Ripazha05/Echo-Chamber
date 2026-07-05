using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "GameScene";

    [Header("Save Detection (untuk Continue)")]
    [SerializeField] private GameObject continueButton;

    [Header("Settings Panel (opsional)")]
    [SerializeField] private GameObject settingsPanel;

    void Start()
    {
        // Cek apakah ada save game. Kalau belum ada sistem save,
        // sembunyikan dulu tombol Continue.
        bool hasSave = PlayerPrefs.HasKey("HasSaveGame");

        if (continueButton != null)
            continueButton.SetActive(hasSave);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OnContinue()
    {
        Debug.Log("Continue ditekan - load save game");
        // TODO: load save data, lalu pindah scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnPlayNewGame()
{
    Debug.Log("Play New Game ditekan");
    PlayerPrefs.SetInt("HasSaveGame", 1);
    FindObjectOfType<FadeTransition>().LoadSceneWithFade("Day 1");
}

    public void OnLoadGame()
    {
        Debug.Log("Load Game ditekan");
        // TODO: buka panel load game, atau langsung load slot terakhir
    }

    public void OnSettings()
    {
        Debug.Log("Settings ditekan");
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OnQuit()
    {
        Debug.Log("Quit ditekan");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}