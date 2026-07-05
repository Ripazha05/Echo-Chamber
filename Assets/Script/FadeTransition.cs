using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    private static FadeTransition instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Fade in saat scene pertama kali dibuka
        StartCoroutine(FadeIn());
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float alpha = timer / fadeDuration;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0);
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        // Fade ke hitam
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1);

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Fade balik dari hitam
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn());
    }

    private void SetAlpha(float alpha)
    {
        if (fadePanel != null)
        {
            Color c = fadePanel.color;
            c.a = alpha;
            fadePanel.color = c;
        }
    }
}