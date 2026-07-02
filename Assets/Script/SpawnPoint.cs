using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string indoorSceneName = "DayScene_Indoor";
    [SerializeField] private string day1SceneName = "Day1";

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
        // Spawn di DayScene_Indoor
        if (scene.name == indoorSceneName)
        {
            StartCoroutine(SpawnAfterDelay("SpawnPoint"));
        }

        // Spawn di Day1 (waktu datang dari MainMenu)
        if (scene.name == day1SceneName)
        {
            StartCoroutine(SpawnAfterDelay("PlayerSpawnStart"));
        }
    }

    System.Collections.IEnumerator SpawnAfterDelay(string spawnTag)
    {
        // Tunggu 1 frame biar scene selesai load dulu
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
            Debug.LogWarning("SpawnPoint dengan tag '" + spawnTag + "' tidak ditemukan!");
        }
    }
}