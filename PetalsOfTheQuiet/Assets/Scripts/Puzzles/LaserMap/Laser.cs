using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private int maxReflections = 10;
    [SerializeField] private float maxDistance = 100f;

    private LineRenderer lineRenderer;

    private Vector3 direction;
    private Vector3 position;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CastLaser();
    }

    void CastLaser()
    {
        direction = startPoint.forward;
        position = startPoint.position;

        if (lineRenderer == null) return;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, position);

         Debug.DrawRay(position, direction * 3f, Color.red, 1f);

        for (int i = 0; i < maxReflections; i++)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit, maxDistance))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                LaserInteract mirror = hit.collider.GetComponent<LaserInteract>();

                if (mirror != null)
                {
                    //direction = Vector3.Reflect(direction, hit.normal);
                    direction = mirror.transform.forward;
                    position = hit.point;
                    continue;
                }

                LaserTarget target = hit.collider.GetComponent<LaserTarget>();

                if (target != null)
                {
                    target.OnLaserHit();
                }

                break;
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position + direction * maxDistance);
                break;
            }
        }
    }

}
