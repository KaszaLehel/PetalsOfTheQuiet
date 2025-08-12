using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject plantingSpot;
    [SerializeField] GameObject VFXPrefab;
    public static GameManager Instance { get; private set; }
    public Dictionary<int, bool> flowerStates = new();
    public bool isEndingMoment = false;
    public bool isSoundOn = false;

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
        DontDestroyOnLoad(gameObject);
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
            //Itt nyitom meg a Planting Spotot.
            plantingSpot.SetActive(true);

            if (VFXPrefab != null)
            {
                Instantiate(VFXPrefab, plantingSpot.transform.position, plantingSpot.transform.rotation);
            }
            else
            {
                Debug.LogWarning("Nincs beállítva VFX prefab!");
            }

            Debug.Log("Minden virag megvan, ultetes aktivalva.");
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
    

    public void TriggerEnding()
    {
        if (isEndingMoment) return;

        isEndingMoment = true;
        OnEndingTriggered?.Invoke();
    }
}
