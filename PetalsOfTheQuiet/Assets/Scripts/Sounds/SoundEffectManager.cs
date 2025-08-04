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
        DontDestroyOnLoad(gameObject);
    }


    public void PlaySoundFX(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlaySoundFXWithDelay(AudioClip audioClip, Transform position, float volume, float delay)
    {
        StartCoroutine(PlaySoundFXTimer(audioClip, position, volume, delay));
    }


    private IEnumerator PlaySoundFXTimer(AudioClip audioClip, Transform transform, float volume, float delay)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;

        yield return new WaitForSeconds(delay);

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
