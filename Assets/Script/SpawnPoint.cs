using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string day1SceneName = "Day 1";

    [SerializeField] private string[] indoorSceneNames = new string[]
    {
        "DayScene_Indoor",
        "Interior_Kelas_Baru_day2",
        "Day 2",
        "Day 3"
    };

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string indoorScene in indoorSceneNames)
        {
            if (scene.name == indoorScene)
            {
                StartCoroutine(SpawnAfterDelay("SpawnPoint", scene.name));
                return;
            }
        }

        if (scene.name == day1SceneName)
        {
            StartCoroutine(SpawnAfterDelay("PlayerSpawnStart", scene.name));
        }
    }

    System.Collections.IEnumerator SpawnAfterDelay(string spawnTag, string sceneName)
    {
        yield return null;

        GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnTag);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
            Debug.Log("Player di-spawn ke: " + spawnPoint.transform.position);
        }
        else
        {
            Debug.LogWarning("SpawnPoint '" + spawnTag + "' tidak ditemukan di scene: " + sceneName);
        }
    }
}