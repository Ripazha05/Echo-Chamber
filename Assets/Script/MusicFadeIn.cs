using UnityEngine;
using System.Collections;

public class MusicFadeIn : MonoBehaviour
{
    [SerializeField] private float targetVolume = 0.4f;
    [SerializeField] private float fadeDuration = 2f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, timer / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
}