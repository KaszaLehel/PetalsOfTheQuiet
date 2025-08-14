using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider sfxSlider;

    private float masterVol;
    private float ambientVol;
    private float sfxVol;

    private void Awake()
    {
        masterVol = PlayerPrefs.GetFloat("masterVolumeValue", 1f);
        ambientVol = PlayerPrefs.GetFloat("ambientVolumeValue", 1f);
        sfxVol = PlayerPrefs.GetFloat("soundFXVolumeValue", 1f);
    }

    void Start()
    {
        if (masterSlider != null) masterSlider.value = masterVol;
        if (ambientSlider != null) ambientSlider.value = ambientVol;
        if (sfxSlider != null) sfxSlider.value = sfxVol;

        SetMasterVolume(masterVol);
        SetAmbientVolume(ambientVol);
        SetSoundFXVolume(sfxVol);
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("masterVolumeValue", level);
    }

    public void SetAmbientVolume(float level)
    {
        audioMixer.SetFloat("ambientVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("ambientVolumeValue", level);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f); 
        PlayerPrefs.SetFloat("soundFXVolumeValue", level);
    }
}
