using Unity.Cinemachine;
using UnityEngine;

public class TopDownSystem : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float FoVMax = 60f;
    [SerializeField] private float FoVMin = 10f;

    private float targetFOV = 60f;
    float zoomSpeed = 10f;

    void Update()
    {
        if (GameManager.Instance.IsFPS) return;

        Movement();
        Rotation();
        CameraZoom();
    }

    void Movement()
    {
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.unscaledDeltaTime;
    }

    void Rotation()
    {
        float rotateDir = 0f;

        if (Input.GetKey(KeyCode.Q)) rotateDir = -1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = +1f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.unscaledDeltaTime, 0);
    }

    void CameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            //virtualCamera.Lens.FieldOfView = 10;
            targetFOV -= 5;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            targetFOV += 5;
        }

        
        targetFOV = Mathf.Clamp(targetFOV, FoVMin, FoVMax);

        virtualCamera.Lens.FieldOfView = Mathf.Lerp(virtualCamera.Lens.FieldOfView, targetFOV, Time.unscaledDeltaTime * zoomSpeed);
    }
}
