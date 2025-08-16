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

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;

    [Header("Light Settings")]
    [SerializeField] private Light pointLight;
    [SerializeField] private float lightFadeDuration = 4f;

    private bool active = false;

    void Start()
    {
        GameManager.Instance.RegisterFlower(ID);

        indicatorE.SetActive(false);

        //if (ambientSounds == null) return;

        if (ambientSounds != null)
        {
            foreach (GameObject ambient in ambientSounds)
            {
                ambient.SetActive(false);
            }
        }
    }

    //Amikor ránéz az interactable gameObjectre
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

        if (pointLight != null)
        {
            StartCoroutine(FadeLight());
        }

        GameManager.Instance.MarkFlowerPicked(ID);

        if (ID == 0)
        {
            GameManager.Instance.isSoundOn = true;
            //Debug.Log($"The flower: {ID} -> was activated the soundFX-es in the GameManager.");
        }

        if (flowerMusic != null)
            SoundEffectManager.Instance.PlaySoundFXWithDelay(flowerMusic, transform, 1f, 2f, ambientSounds);
        else
            Debug.LogWarning("No flowerMusic on this OBject");

        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        Destroy(gameObject, 10f);
    }


    private IEnumerator FadeLight()
    {
        float intensity = pointLight.intensity;
        float elapsed = 0f;

        while (elapsed < lightFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lightFadeDuration;

            pointLight.intensity = Mathf.Lerp(intensity, 0f, t);

            yield return null;
        }

        pointLight.intensity = 0f;
    }
}
