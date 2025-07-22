using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] Camera cameraMain;
    [SerializeField] Camera cameraTopDown;

    bool isCameraMain = true;

    void Start()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTopDown.gameObject.SetActive(false);
    }

    public void ManageCamera()
    {
        if (isCameraMain)
        {
            CamToDown();
            isCameraMain = false;
        }
        else
        {
            CamMain();
            isCameraMain = true;
        }
    }

    void CamMain()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTopDown.gameObject.SetActive(false);
    }

    void CamToDown()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTopDown.gameObject.SetActive(true);
    }
}
