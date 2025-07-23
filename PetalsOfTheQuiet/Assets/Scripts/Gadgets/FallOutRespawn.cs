using UnityEngine;

public class FallOutRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float minY = -10f;
    [SerializeField] private float rayStartHeight = 100f;
    [SerializeField] private float rayDistance = 200f;
    [SerializeField] private float heightOffset = 1f;
    [SerializeField] private float worldEdgeLimit = 45f;
    [SerializeField] private float returnForce = 10f;

     private Rigidbody rb;
    private static readonly Vector3 center = Vector3.zero;  

     void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < minY)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        Vector3 baseXZ = new Vector3(
            Mathf.Clamp(transform.position.x, -worldEdgeLimit, worldEdgeLimit),
            rayStartHeight,
            Mathf.Clamp(transform.position.z, -worldEdgeLimit, worldEdgeLimit)
        );

        Vector3 targetPosition = baseXZ;

        if (Physics.Raycast(baseXZ, Vector3.down, out RaycastHit hit, rayDistance))
        {
            targetPosition.y = hit.point.y + heightOffset;
        }
        else
        {
            targetPosition.y = heightOffset;
        }

        transform.position = targetPosition;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 arcDirection = (center - transform.position + Vector3.up).normalized;
        rb.AddForce(arcDirection * returnForce, ForceMode.VelocityChange);
    }
}
