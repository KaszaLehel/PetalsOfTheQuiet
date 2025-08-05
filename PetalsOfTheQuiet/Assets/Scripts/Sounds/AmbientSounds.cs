using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] private Collider Area;
    [SerializeField] private GameObject Player;
    [SerializeField] private float smoothingSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);

    private Vector3 targetPosition;

    void Update()
    {
        if (Area == null || Player == null) return;

        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        targetPosition = closestPoint + offset;

        //transform.position = closestPoint;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
    }
}
