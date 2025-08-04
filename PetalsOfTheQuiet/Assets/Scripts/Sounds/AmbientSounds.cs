using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] private Collider Area;
    [SerializeField] private GameObject Player;

    void Update()
    {
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        transform.position = closestPoint;
    }
}
