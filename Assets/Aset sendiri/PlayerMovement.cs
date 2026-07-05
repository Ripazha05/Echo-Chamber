using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Referensi")]
    public Transform cameraTransform;
    public Animator animator;

    [Header("Animasi")]
    public string runParameterName = "IsRunning";

    // Virtual joystick input (diisi otomatis dari JoystickInput script)
    [HideInInspector] public Vector2 joystickInput = Vector2.zero;

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
        velocity.y = -2f;

    // Gabungkan input keyboard + joystick
    float horizontal = Input.GetAxisRaw("Horizontal") + joystickInput.x;
    float vertical = Input.GetAxisRaw("Vertical") + joystickInput.y;

    horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    vertical = Mathf.Clamp(vertical, -1f, 1f);

    Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;
    bool isMoving = inputDir.magnitude >= 0.1f;

    if (animator != null)
        animator.SetBool(runParameterName, isMoving);

    if (isMoving && cameraTransform != null)
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Pastikan X dan Z keduanya dipakai
        Vector3 moveDir = (camForward * vertical) + (camRight * horizontal);
        moveDir.Normalize();

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
}
}