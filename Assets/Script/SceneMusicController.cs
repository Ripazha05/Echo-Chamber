using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    void Awake()
    {
        // Cari dan matikan musik dari MainMenu kalau masih ada
        GameObject mainMenuMusic = GameObject.Find("MainMenuMusic");
        if (mainMenuMusic != null)
            mainMenuMusic.SetActive(false);
    }
}