using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    private Dictionary<string, bool> puzzleCompletion = new();
    public bool allPuzzleCompleted = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PuzzleComplete(string id)
    {
        if (puzzleCompletion.ContainsKey(id)) return;

        puzzleCompletion.TryAdd(id, true);
        Debug.Log($"Pizzle completed: {id}");

        /*if (!puzzleCompletion.ContainsKey(id))
        {
            puzzleCompletion.TryAdd(id, true);
            Debug.Log($"Pizzle completed: {id}");
        }
        else
        {
            Debug.Log("Its Already Exist");
        }*/

        if (puzzleCompletion.Count == 3 && AllPuzzlesAreCompleted())
        {
            allPuzzleCompleted = true;
            Debug.Log("All puzzle is completed!");
        }
    }

    private bool AllPuzzlesAreCompleted()
    {
        foreach (var kvp in puzzleCompletion)
        {
            if (!kvp.Value) return false;
        }
        return true;
    }
}
