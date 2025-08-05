using System.Collections;
using UnityEngine;

public class FlowerInteractable : MonoBehaviour, IInteractable
{
    [Header("ID SEttings")]
    [SerializeField] private int ID;
    [SerializeField] private GameObject indicatorE;
    [SerializeField] private AudioClip pickupSoundFX;
    [SerializeField] private AudioClip flowerMusic;

    [Header("Ambient Sound Settings")]
    [SerializeField] private GameObject[] ambientSounds;

    void Start()
    {
        GameManager.Instance.RegisterFlower(ID);
        indicatorE.SetActive(false);

        if (ambientSounds == null) return;

        foreach (GameObject ambient in ambientSounds)
        {
            ambient.SetActive(false);
        }
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

        //if (ID != 0)
        //SoundEffectManager.Instance.PlaySoundFX(pickupSoundFX, transform, 1f);

        if (ID == 0)
        {
            GameManager.Instance.isSoundOn = true;
            Debug.Log($"The flower: {ID} -> was activated the soundFX-es in the GameManager.");
        }
           

        if (flowerMusic != null)
                SoundEffectManager.Instance.PlaySoundFXWithDelay(flowerMusic, transform, 1f, 2f, ambientSounds);
            else
                Debug.LogWarning("No flowerMusic on this Object");

        Destroy(gameObject);
    }
}
