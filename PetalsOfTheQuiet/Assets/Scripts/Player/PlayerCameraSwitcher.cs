using UnityEngine;
using UnityEngine.Events;

public class PlayerCameraSwitcher : MonoBehaviour
{
    public UnityEvent manageCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            manageCamera.Invoke();
        }
    }
}
