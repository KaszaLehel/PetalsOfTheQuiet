using Unity.Cinemachine;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Settings")]
    [SerializeField] private float grabDistance = 3f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private LayerMask grabbableLayer;

    private Rigidbody grabbedObject;

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
            MoveGrabbedObject();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb != null && !rb.isKinematic)
            {
                grabbedObject = rb;
                grabbedObject.useGravity = false;
                grabbedObject.linearDamping = 10f;
                grabbedObject.angularDamping = 10f;
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.linearDamping = 0f;
            grabbedObject.angularDamping = 0.05f;
            grabbedObject = null;
        }
    }

    void MoveGrabbedObject()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * grabDistance;
        Vector3 direction = targetPosition - grabbedObject.position;
        grabbedObject.linearVelocity = direction * moveSpeed;
    }
}
