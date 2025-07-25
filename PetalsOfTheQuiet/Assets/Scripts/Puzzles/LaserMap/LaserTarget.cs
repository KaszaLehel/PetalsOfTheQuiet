using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    [SerializeField] private string id = "thirdPuzzle";
    [SerializeField] private GameObject flower;
    [SerializeField] private GameObject LaserObject;
    private bool isActivated = false;

    void Start()
    {
        flower.SetActive(false);
    }

    public void OnLaserHit()
    {
        if (isActivated) return;

        isActivated = true;
        flower.SetActive(true);
        PuzzleManager.Instance.PuzzleComplete(id);
        
        Destroy(LaserObject, 3);
    }
}
