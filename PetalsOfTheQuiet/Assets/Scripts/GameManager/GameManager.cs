using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject plantingSpot;
    public static GameManager Instance { get; private set; }
    public Dictionary<int, bool> flowerStates = new();
    

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

        if (flowerStates.Count == 4 && flowerStates.Values.All(state => state))
        {
            plantingSpot.SetActive(true);
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
}
