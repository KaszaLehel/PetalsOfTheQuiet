using System.Collections;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundObject;
    public static SoundEffectManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void PlaySoundFX(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        //float clipLength = audioSource.clip.length;
        float clipLength = audioClip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlaySoundFXWithDelay(AudioClip audioClip, Transform position, float volume, float delay, GameObject[] ambeintSounds = null, bool is2D = false)
    {
        if(ambeintSounds != null)
            ActivateAmbient(ambeintSounds);

        StartCoroutine(PlaySoundFXTimer(audioClip, position, volume, delay, is2D));
    }


    private IEnumerator PlaySoundFXTimer(AudioClip audioClip, Transform transform, float volume, float delay, bool is2D)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.spatialBlend = is2D ? 0f : 1f;

        yield return new WaitForSeconds(delay);

        audioSource.Play();

        //float clipLength = audioSource.clip.length;
        float clipLength = audioClip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    private void ActivateAmbient(GameObject[] ambientSounds)
    {
        foreach (GameObject ambient in ambientSounds)
        {
            ambient.SetActive(true);
        }
    }
}
