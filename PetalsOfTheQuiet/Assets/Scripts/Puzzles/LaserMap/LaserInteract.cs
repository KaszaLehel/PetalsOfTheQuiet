using UnityEngine;

public class LaserInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject indicatorE;
    private int rotationStep = 45;
    private int currentStep = 0;
    
    void Start()
    {
        indicatorE.SetActive(false);
    }

    public void OnFocusEnter()
    {
        if (indicatorE != null)
            indicatorE.SetActive(true);
    }
    public void OnFocusExit()
    {
        if (indicatorE != null)
            indicatorE.SetActive(false);
    }

    public void OnInteract()
    {
        currentStep = (currentStep + 1) % 8;
        transform.rotation = Quaternion.Euler(0f, currentStep * rotationStep, 0f);
    }

    
}
