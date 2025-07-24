using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    /*void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }*/

    void LateUpdate()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
