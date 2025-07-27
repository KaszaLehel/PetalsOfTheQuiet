using UnityEngine;

public class FlowerInteractable : MonoBehaviour, IInteractable
{
    [Header("ID SEttings")]
    [SerializeField] private int ID;
    [SerializeField] private GameObject indicatorE;

    void Start()
    {
        GameManager.Instance.RegisterFlower(ID);
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
        GameManager.Instance.MarkFlowerPicked(ID);
        Destroy(gameObject);
    }
}
