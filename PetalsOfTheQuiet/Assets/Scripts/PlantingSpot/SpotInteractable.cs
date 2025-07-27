using UnityEngine;

public class SpotInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject indicatorE;


    void Start()
    {
        gameObject.SetActive(false);
        indicatorE.SetActive(false);
    }

    //Amikor ránéz az interactable gameObjectre
    public void OnFocusEnter()
    {
        if (indicatorE != null)
            indicatorE.SetActive(true);
    }

    //AMikor lenéz az interactable gameObjectről
    public void OnFocusExit()
    {
        if (indicatorE != null)
            indicatorE.SetActive(false);
    }

    //Amikor rá van nézve és megnyomja az Interact(E) betűt
    public void OnInteract()
    {
        Debug.Log("Elultetve es vege a jateknak");

    }
}
