using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 10f;

    [Header("Mouse Look Settings")]
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;

    [Header("World Border Settings")]
    [SerializeField] private float worldLimit = 50f;
    [SerializeField] private float pushBackSpeed = 5f;
    [SerializeField] private float pushBackDuration = 1.5f;
    [SerializeField] private float rotateSpeed = 1.2f;
    [SerializeField] private float lookResetSpeed = 2f;
    [SerializeField] private Vector3 worldCenter = Vector3.zero;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    bool canMove = true;
    bool isPushingBack = false;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isPushingBack) { return; }
        CheckWorldBorder();
        Movement();

    }

    //Camera movement
    void LateUpdate()
    {
        Rotation();
    }

    #region Movement > Jumping Method
    void Movement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        Jumping(movementDirectionY, isRunning);


    }
    #endregion

    #region Jumping
    void Jumping(float _movementDirectionY, bool _isRunning)
    {
        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = _movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }
    #endregion

    #region  Rotation
    void Rotation()
    {
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            fpsCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    #endregion

    #region Border Checking
    void CheckWorldBorder()
    {
        Vector3 position = transform.position;
        if (Mathf.Abs(position.x) > worldLimit || Mathf.Abs(position.z) > worldLimit)
        {
            StartCoroutine(PushBackToCenter());
        }
    }
    #endregion

    #region  IEnumerators

    IEnumerator PushBackToCenter()
    {
        isPushingBack = true;
        canMove = false;

        Vector3 direction = (worldCenter - transform.position).normalized;

        float timer = 0f;
        while (timer < pushBackDuration)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

            rotationX = Mathf.Lerp(rotationX, 0f, Time.deltaTime * lookResetSpeed);
            fpsCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            else
            {
                moveDirection.y = -1f;
            }

            Vector3 move = direction * pushBackSpeed;
            move.y = moveDirection.y;

            characterController.Move(move * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isPushingBack = false;
        canMove = true;
    }
    #endregion
    
    void OnDrawGizmosSelected()
    {
    Gizmos.color = Color.cyan;

    Vector3 size = new Vector3(worldLimit * 2, 0.1f, worldLimit * 2);
    Gizmos.DrawWireCube(worldCenter, size);
    }
}
