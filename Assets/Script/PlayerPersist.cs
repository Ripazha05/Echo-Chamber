using UnityEngine;

public class PlayerPersist : MonoBehaviour
{
    private static PlayerPersist instance;

    [SerializeField] private GameObject mainCamera; // drag Main Camera ke sini

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Kamera juga jangan dihapus saat ganti scene
            if (mainCamera != null)
                DontDestroyOnLoad(mainCamera);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}