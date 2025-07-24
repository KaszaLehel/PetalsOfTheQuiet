using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
     [Header("Important")]
    [SerializeField] private Camera playerCamera;

    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private float sphereRadius = 0.3f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask grabLayer;

    [Header("Grab Settings")]
    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody grabbedObject;
    private IInteractable currentInteractable;
    private RaycastHit hitRay;
    private RaycastType currentRaycastType = RaycastType.None;


    private enum RaycastType
    {
        None,
        Interact,
        Grab,
        Paint
    }

    void Update()
    {
        PerformRaycast();
        HandleInput();
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
            MoveGrabbedObject();
    }

    private void PerformRaycast()
    {
        currentRaycastType = RaycastType.None;
        HideCurrentIndicator();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.SphereCast(ray, sphereRadius, out hitRay, rayDistance, interactLayer))
        {
            currentRaycastType = RaycastType.Interact;

            IInteractable interactable = hitRay.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                //Debug.Log(currentRaycastType);
                currentInteractable.OnFocusEnter();
            }
            return;
        }

        if (Physics.SphereCast(ray, sphereRadius, out hitRay, rayDistance, grabLayer))
        {
            currentRaycastType = RaycastType.Grab;
            //Debug.Log(currentRaycastType);
            return;
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentRaycastType == RaycastType.Grab) TryGrab();
        }

        if (Input.GetMouseButtonUp(0)) Release();

        if (Input.GetKeyDown(KeyCode.E) && currentRaycastType == RaycastType.Interact && currentInteractable != null)
        {
            TryInteract();
        }
    }

    private void TryGrab()
    {
        if (grabbedObject != null) return;

        Rigidbody rb = hitRay.rigidbody;
        if (rb != null && !rb.isKinematic)
        {
            grabbedObject = rb;
            grabbedObject.useGravity = false;
            grabbedObject.linearDamping = 10f;
            grabbedObject.angularDamping = 10f;
        }
    }

    private void Release()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.linearDamping = 0f;
            grabbedObject.angularDamping = 0.05f;
            grabbedObject = null;
        }
    }

    private void TryInteract()
    {
        currentInteractable?.OnInteract();
    }

    private void MoveGrabbedObject()
    {
        Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * rayDistance;
        Vector3 direction = targetPos - grabbedObject.position;
        grabbedObject.linearVelocity = direction * moveSpeed;
    }

    private void HideCurrentIndicator()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnFocusExit();
            currentInteractable = null;
        }
    }
}
