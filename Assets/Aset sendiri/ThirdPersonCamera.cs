using UnityEngine;

// Pasang script ini ke GameObject "Main Camera".
// Isi field "Target" dengan karakter yang mau diikuti (misalnya "ManUpdate2").
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("ManUpdate2")]
    public Transform ManUpdate2;
    public Vector3 ManUpdate2Offset = new Vector3(0f, 1.5f, 0f);

    [Header("Jarak Kamera")]
    public float distance = 4f;
    public float minDistance = 1.5f;
    public float maxDistance = 8f;
    public float zoomSpeed = 4f;

    [Header("Rotasi (Mouse Look)")]
    public float mouseSensitivity = 3f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    [Header("Collision")]
    [Tooltip("Supaya kamera tidak menembus tembok/lantai")]
    public float collisionOffset = 0.3f;
    public LayerMask collisionMask = ~0;

    private float yaw;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (ManUpdate2 != null)
            yaw = ManUpdate2.eulerAngles.y;
    }

    void Update()
    {
        if (ManUpdate2 == null) return;

        // Tekan Escape buat lepas/kunci cursor lagi (enak waktu testing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isLocked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = isLocked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isLocked;
        }

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (ManUpdate2 == null) return;

        Vector3 focusPoint = ManUpdate2.position + ManUpdate2Offset;
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        float finalDistance = distance;

        // Cegah kamera nembus dinding/lantai
        if (Physics.Linecast(focusPoint, focusPoint - rotation * Vector3.forward * distance, out RaycastHit hit, collisionMask))
        {
            finalDistance = Mathf.Clamp(hit.distance - collisionOffset, minDistance, distance);
        }

        Vector3 finalPosition = focusPoint - rotation * Vector3.forward * finalDistance;

        transform.position = finalPosition;
        transform.rotation = rotation;
    }
}