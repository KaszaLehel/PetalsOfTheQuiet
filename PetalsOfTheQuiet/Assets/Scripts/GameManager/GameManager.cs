using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum CameraMode
    {
        FPS,
        TopDown
    }

    public CameraMode CurrentCameraMode { get; private set; } = CameraMode.FPS;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleCameraMode()
    {
        if (CurrentCameraMode == CameraMode.FPS)
            SetCameraMode(CameraMode.TopDown);
        else
            SetCameraMode(CameraMode.FPS);
    }

    public void SetCameraMode(CameraMode mode)
    {
        CurrentCameraMode = mode;
    }

    public bool IsFPS => CurrentCameraMode == CameraMode.FPS;
    public bool IsTopDown => CurrentCameraMode == CameraMode.TopDown;
}
