using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject plantingSpot;
    [SerializeField] GameObject VFXPrefab;

    [SerializeField] private Image fadeImage;
    [SerializeField] private TMP_Text endingText;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private float activateFadeTime = 0.5f;

    public static GameManager Instance { get; private set; }

    public Dictionary<int, bool> flowerStates = new();
    public bool isEndingMoment = false;
    public bool isSoundOn = false;

    public bool isGamePaused = false;

    public static event Action OnFlowerPicked;
    public static event Action OnEndingTriggered;

    public enum CameraMode
    {
        FPS,
        TopDown
    }
    public CameraMode CurrentCameraMode { get; private set; } = CameraMode.FPS;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        fadeImage.gameObject.SetActive(false);
        endingText.gameObject.SetActive(false);
    }


    #region Flower Pickup System
    public void RegisterFlower(int id)
    {
        if (!flowerStates.ContainsKey(id))
            flowerStates.Add(id, false);
    }

    public void MarkFlowerPicked(int id)
    {
        if (flowerStates.ContainsKey(id))
            flowerStates[id] = true;

        OnFlowerPicked?.Invoke();

        if (flowerStates.Count == 4 && flowerStates.Values.All(state => state))
        {
            //Open up the Planting Spot.
            plantingSpot.SetActive(true);
            Debug.Log("Every Flower is in the bag, PlantingSpot activated.");
        }
    }
    #endregion

    #region Camera System
    public void SetCameraMode(CameraMode mode)
    {
        CurrentCameraMode = mode;
    }

    public bool IsFPS => CurrentCameraMode == CameraMode.FPS;
    public bool IsTopDown => CurrentCameraMode == CameraMode.TopDown;

    #endregion


    public void TriggerEnding(float endMusicLength)
    {
        if (isEndingMoment) return;
        isEndingMoment = true;

        float waitTime = endMusicLength * activateFadeTime;

        OnEndingTriggered?.Invoke();

        StartCoroutine(EndingSequence(waitTime));
        StartCoroutine(LoadSceneAfterMusic(endMusicLength));
    }

    private IEnumerator LoadSceneAfterMusic(float delay)
    {
        float fiveSec = 5f;
        yield return new WaitForSeconds(fiveSec);
        
        if (VFXPrefab != null)
        {
            Instantiate(VFXPrefab, plantingSpot.transform.position, plantingSpot.transform.rotation);
        }
        else
        {
            Debug.LogWarning("Nincs beállítva VFX prefab!");
        }

        yield return new WaitForSeconds(delay - fiveSec);
        TransitionController.Instance.NextScene("Menu");
    }


    private IEnumerator EndingSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        fadeImage.gameObject.SetActive(true);
        endingText.gameObject.SetActive(false);

        float startAlpha = 0f;
        float endAlpha = 1f;
        float elapsed = 0f;

        Color myColor = fadeImage.color;
        myColor.a = startAlpha;
        fadeImage.color = myColor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeDuration;

            myColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = myColor;

            yield return null;
        }
        myColor.a = endAlpha;
        fadeImage.color = myColor;

        yield return new WaitForSeconds(1.5f);

        endingText.gameObject.SetActive(true);
    }
}
