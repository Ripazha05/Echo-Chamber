using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Joystick Settings")]
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private float joystickRange = 50f;

    private PlayerMovement playerMovement;
    private Vector2 inputVector = Vector2.zero;

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
        CariPlayer();
        ResetJoystickPosition(); // Pastikan posisi reset saat ganti scene
    }

    void Start()
    {
        CariPlayer();
        ResetJoystickPosition(); // Paksa handle ke tengah saat game dimulai
    }

    void ResetJoystickPosition()
    {
        if (joystickHandle != null)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
        }
        inputVector = Vector2.zero;
    }

    void CariPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerMovement = playerObj.GetComponent<PlayerMovement>();
            Debug.Log("Joystick: Player ditemukan — " + playerObj.name);
        }
        else
        {
            Debug.LogWarning("Joystick: Player tidak ditemukan!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerMovement == null) CariPlayer();
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (playerMovement == null || joystickBackground == null || joystickHandle == null) return;

        Vector2 position;
        
        // Menggunakan null untuk kamera jika Canvas bertipe Screen Space - Overlay agar kalkulasi koordinat UI akurat
        Camera uiCamera = eventData.pressEventCamera;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, uiCamera, out position))
        {
            position = Vector2.ClampMagnitude(position, joystickRange);
            joystickHandle.anchoredPosition = position; // Menggunakan anchoredPosition lebih stabil untuk UI Canvas

            inputVector = position / joystickRange;
            playerMovement.joystickInput = inputVector;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystickPosition();

        if (playerMovement != null)
            playerMovement.joystickInput = Vector2.zero;
    }
}