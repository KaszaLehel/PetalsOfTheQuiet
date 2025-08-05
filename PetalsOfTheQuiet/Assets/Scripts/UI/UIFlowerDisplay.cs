using UnityEngine;

public class UIFlowerDisplay : MonoBehaviour
{
    [SerializeField] private GameObject flowerIconPrefab;
    [SerializeField] private Transform gridParent;

    void OnEnable()
    {
        GameManager.OnFlowerPicked += UpdateFlowerUI;
    }

    void OnDisable()
    {
        GameManager.OnFlowerPicked -= UpdateFlowerUI;
    }

    public void UpdateFlowerUI()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var kvp in GameManager.Instance.flowerStates)
        {
            if (kvp.Value)
            {
                Instantiate(flowerIconPrefab, gridParent);
            }
        }
    }
}
