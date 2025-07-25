using UnityEngine;

public class PicturePuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private string puzzleID;
    [SerializeField] private GameObject flowerToActivate;
    private bool isSolved = false;

    void Start()
    {
        flowerToActivate.SetActive(false);
    }

    public void OnPictureInteracted(GameObject pictureObject)
    {
        if (isSolved) return;

        GoodImage goodImage = pictureObject.GetComponent<GoodImage>();

        if (goodImage == null) return;

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
