using System.Collections;
using UnityEngine;

public class FireFlyInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject indicatorE;
    [SerializeField] private AudioClip story;

    [SerializeField] private ParticleSystem ps;
    private bool isFading = false;
    private bool active = false;

    void Start()
    {
        indicatorE.SetActive(false);

        //ps = GetComponent<ParticleSystem>();
        //if (ps == null)
        //{
           //Debug.LogError("Nincs Particlesystem komponens ezen az objektumon");
        //}
    }

    public void OnFocusEnter()
    {
        if (active) return;
        
        if (indicatorE != null)
            indicatorE.SetActive(true);
    }

    //AMikor lenéz az interactable gameObjectről
    public void OnFocusExit()
    {
        if (active) return;

        if (indicatorE != null)
            indicatorE.SetActive(false);
    }

    //Amikor rá van nézve és megnyomja az Interact(E) betűt
    public void OnInteract()
    {
        if (active) return;

        active = true;

        indicatorE.SetActive(false);

        StartFadeOut();

        if (story != null)
        {
            SoundEffectManager.Instance.PlaySoundFX(story, transform, 1f);
            
        }
        else
        {
            Debug.Log("No story AudioClip");
        }

        Destroy(gameObject, 5);
    }


    public void StartFadeOut()
    {
        if (ps == null || isFading)
            return;
        //Debug.Log("Fading PS");
        isFading = true;

        var emission = ps.emission;
        emission.enabled = false;
    }
}
