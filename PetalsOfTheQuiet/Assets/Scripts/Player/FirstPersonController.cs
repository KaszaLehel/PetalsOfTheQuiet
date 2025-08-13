using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravity = -10f;

    [Header("Ground Check")]
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float GroundedRadius = 0.5f;
    [SerializeField] private LayerMask GroundLayers;

    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float topClamp = 90f;
    [SerializeField] private float bottomClamp = -90f;

    [Header("World Border Settings")]
    [SerializeField] private float worldLimit = 50f;
    [SerializeField] private float pushBackSpeed = 5f;
    [SerializeField] private float pushBackDuration = 1.5f;
    [SerializeField] private float rotateSpeed = 1.2f;
    [SerializeField] private float lookResetSpeed = 2f;
    [SerializeField] private Vector3 worldCenter = Vector3.zero;

    [Header("Ending Scene Settings")]
    [SerializeField] private Transform endingViewTarget;
    [SerializeField] private float endingMoveSpeed = 2f;
    [SerializeField] private float endingRotateSpeed = 1.5f;
    [SerializeField] private float cameraLookSpeed = 2f;

    private CharacterController controller;
    private float verticalVelocity;
    private float terminalVelocity = 53f;
    private bool grounded;
    private float pitch;
    private bool isPushingBack = false;
    private bool isEndingViewStarted = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        GameManager.OnEndingTriggered += HandleEndingTriggered;
    }

    void OnDisable()
    {
        GameManager.OnEndingTriggered -= HandleEndingTriggered;
    }

    void Update()
    {
        if (GameManager.Instance.IsTopDown || isPushingBack || GameManager.Instance.isEndingMoment) return;

        //GroundedCheck();
        HandleMovement();
        HandleJump();
        CheckWorldBorder();
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IsTopDown || isPushingBack || GameManager.Instance.isEndingMoment) return;

        HandleCamera();
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        if (verticalVelocity < terminalVelocity)//(!grounded) -> ezzel nem jol mukodik.
        {
            verticalVelocity += gravity * Time.deltaTime;
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
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, bottomClamp, topClamp);

        cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void CheckWorldBorder()
    {
        Vector3 position = transform.position;
        if (Mathf.Abs(position.x) > worldLimit || Mathf.Abs(position.z) > worldLimit)
        {
            StartCoroutine(PushBackToCenter());
        }
    }

    private void HandleEndingTriggered()
    {
        if (!isEndingViewStarted)
        {
            StartCoroutine(EndingView());
            isEndingViewStarted = true;
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

            verticalVelocity += gravity * Time.deltaTime;

            Vector3 move = direction * pushBackSpeed;
            move.y = verticalVelocity;

            controller.Move(move * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isPushingBack = false;
    }

    IEnumerator EndingView()
    {
        verticalVelocity = 0f;

        float distanceThreshold = 0.1f;
        bool reachedTarget = false;

        while (!reachedTarget)
        {
            Vector3 toTarget = endingViewTarget.position - transform.position;
            Vector3 horizontalMove = new Vector3(toTarget.x, 0, toTarget.z);
            Vector3 move = horizontalMove.normalized * endingMoveSpeed;

            controller.Move(move * Time.deltaTime);

            Vector3 lookDir = worldCenter - transform.position;
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * endingRotateSpeed);
            }

            pitch = Mathf.Lerp(pitch, 0f, Time.deltaTime * cameraLookSpeed);
            cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            if (horizontalMove.magnitude < distanceThreshold)
            {
                reachedTarget = true;
            }

            yield return null;
        }

        Debug.Log("EndingView elérve, most jöhet zene, UI, fade, stb.");
    }

    void OnDrawGizmos()
    {
        Color color = grounded ? new Color(0, 1, 0, 1f) : new Color(1, 0, 0, 1f);
        Gizmos.color = color;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        Gizmos.DrawSphere(spherePosition, GroundedRadius);
        
        Gizmos.color = Color.red;
        Vector3 size = new Vector3(worldLimit * 2, 0.1f, worldLimit * 2);
        Gizmos.DrawWireCube(worldCenter, size);
    }
}
