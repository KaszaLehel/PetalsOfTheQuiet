using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera primaryFPSCamera;
    [SerializeField] CinemachineCamera secondaryTopDownCamera;

    private bool usingTopDown = false;

    void Start()
    {
        SetCameraState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            usingTopDown = !usingTopDown;
            SetCameraState(usingTopDown);
        }
    }

    void SetCameraState(bool topDownActive)
    {
        secondaryTopDownCamera.gameObject.SetActive(topDownActive);
        primaryFPSCamera.gameObject.SetActive(!topDownActive);

        GameManager.Instance.SetCameraMode(topDownActive ? GameManager.CameraMode.TopDown : GameManager.CameraMode.FPS);
    }

    /*void SetCameraState(bool topDownActive)
    {
        secondaryTopDownCamera.gameObject.SetActive(topDownActive);
        primaryFPSCamera.gameObject.SetActive(!topDownActive);
    }*/


}
