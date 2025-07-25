using UnityEngine;

public class ImageInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject indicatorE;
    //private PicturePuzzle picturePuzzle;
    [SerializeField] private PicturePuzzle picturePuzzle;


    void Start()
    {
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
        picturePuzzle?.OnPictureInteracted(this.gameObject);
        //Destroy(gameObject);
    }
}
