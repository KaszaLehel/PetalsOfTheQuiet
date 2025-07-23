using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float SprintSpeed = 7f;
    [SerializeField] private float RotationSpeed = 1f;
    [SerializeField] private float JumpHeight = 0.5f;
    [SerializeField] private float Gravity = -10f;

    [Header("Ground Check")]
    [SerializeField] private float GroundedOffset = -0.14f;
    [SerializeField] private float GroundedRadius = 0.5f;
    [SerializeField] private LayerMask GroundLayers;

    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float TopClamp = 90f;
    [SerializeField] private float BottomClamp = -90f;

    [Header("World Border Settings")]
    [SerializeField] private float worldLimit = 50f;
    [SerializeField] private float pushBackSpeed = 5f;
    [SerializeField] private float pushBackDuration = 1.5f;
    [SerializeField] private float rotateSpeed = 1.2f;
    [SerializeField] private float lookResetSpeed = 2f;
    [SerializeField] private Vector3 worldCenter = Vector3.zero;

    private CharacterController controller;
    private float verticalVelocity;
    private float terminalVelocity = 53f;
    private bool grounded;
    private float pitch;
    bool isPushingBack = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (GameManager.Instance.IsTopDown || isPushingBack) return;

        GroundedCheck();
        HandleMovement();
        HandleJump();
        CheckWorldBorder();
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IsTopDown || isPushingBack) return;

        HandleCamera();
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : MoveSpeed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        if (verticalVelocity < terminalVelocity)//(!grounded)
        {
            verticalVelocity += Gravity * Time.deltaTime;
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        move.y = verticalVelocity;

        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, BottomClamp, TopClamp);

        cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void CheckWorldBorder()
    {
        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x) > worldLimit || Mathf.Abs(pos.z) > worldLimit)
        {
            StartCoroutine(PushBackToCenter());
        }
    }

    IEnumerator PushBackToCenter()
    {
        isPushingBack = true;
        verticalVelocity = 0f;

        Vector3 direction = (worldCenter - transform.position).normalized;
        float timer = 0f;

        while (timer < pushBackDuration)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

            pitch = Mathf.Lerp(pitch, 0f, Time.deltaTime * lookResetSpeed);
            cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            verticalVelocity += Gravity * Time.deltaTime;

            Vector3 move = direction * pushBackSpeed;
            move.y = verticalVelocity;

            controller.Move(move * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isPushingBack = false;
    }

    void OnDrawGizmos()
    {
        Color color = grounded ? new Color(0, 1, 0, 0.35f) : new Color(1, 0, 0, 0.35f);
        Gizmos.color = color;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Gizmos.DrawSphere(spherePosition, GroundedRadius);
        
        Gizmos.color = Color.cyan;
        Vector3 size = new Vector3(worldLimit * 2, 0.1f, worldLimit * 2);
        Gizmos.DrawWireCube(worldCenter, size);
    }

}
