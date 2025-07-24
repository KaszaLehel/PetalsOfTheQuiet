using UnityEngine;

public class FirstPuzzle : MonoBehaviour
{
    [SerializeField] private string id = "firstPuzzle";
    [SerializeField] private PuzzleTable[] sockets;
    [SerializeField] private GameObject flowerToActivate;

    private bool isSolved = false;

    public void CheckSolved()
    {
        if (isSolved) return;

        foreach (var socket in sockets)
        {
            if (!socket.IsFilledCorrectly())
                return;
        }
        Debug.Log("First Puzzle is Finished.");
        isSolved = true;
        PuzzleManager.Instance.PuzzleComplete(id);

        if (flowerToActivate != null)
            flowerToActivate.SetActive(true);
    }
}
