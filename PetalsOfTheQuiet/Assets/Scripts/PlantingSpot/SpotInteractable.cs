using System.Collections;
using UnityEngine;

public class SpotInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject indicatorE;
    [SerializeField] private AudioClip endingMusic;

    private float endMusicLength;

    void Start()
    {
        gameObject.SetActive(false);
        indicatorE.SetActive(false);
        endMusicLength = endingMusic.length;
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
        GameManager.Instance.TriggerEnding(endMusicLength);
        SoundEffectManager.Instance.PlaySoundFXWithDelay(endingMusic, transform, 1f, 2f, null, true);
    }
}
