using UnityEngine;

public class PicturePuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private string puzzleID;
    [SerializeField] private GameObject flowerToActivate;
    [SerializeField] private AudioClip stonePickupFX; 
    private bool isSolved = false;

    void Start()
    {
        flowerToActivate.SetActive(false);
    }

    public void OnPictureInteracted(GameObject pictureObject)
    {
        if (isSolved || pictureObject == null) return;

        if (!pictureObject.TryGetComponent(out GoodImage goodImage)) return;

        if(GameManager.Instance.isSoundOn)
            SoundEffectManager.Instance.PlaySoundFX(stonePickupFX, transform, 1f);

        pictureObject.SetActive(false);

        if (goodImage.isCorrectImage)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        isSolved = true;

        if (flowerToActivate != null)
            flowerToActivate.SetActive(true);

        PuzzleManager.Instance.PuzzleComplete(puzzleID);
    }
}
