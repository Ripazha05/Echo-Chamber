using UnityEngine;

// Pasang script ini ke GameObject karakter (misalnya "ManUpdate2").
// GameObject karakter WAJIB punya komponen CharacterController.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Referensi")]
    [Tooltip("Kosongkan saja, otomatis ambil Main Camera kalau tidak diisi")]
    public Transform cameraTransform;
    [Tooltip("Kosongkan saja, otomatis ambil Animator di GameObject ini kalau tidak diisi")]
    public Animator animator;

    [Header("Animasi")]
    [Tooltip("Nama parameter Bool di Animator Controller yang dipakai untuk state lari")]
    public string runParameterName = "IsRunning";

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f; // kecil supaya tetap "nempel" tanah

        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D atau panah kiri-kanan
        float vertical = Input.GetAxisRaw("Vertical");     // W/S atau panah atas-bawah

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;
        bool isMoving = inputDir.magnitude >= 0.1f;

        if (animator != null)
            animator.SetBool(runParameterName, isMoving);

        if (isMoving && cameraTransform != null)
        {
            // Hitung arah gerak relatif terhadap arah hadap kamera (bukan world axis)
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            // Putar karakter menghadap arah gerak
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Lompat (spasi)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}