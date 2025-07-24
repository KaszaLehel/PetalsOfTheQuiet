using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public static PuzzleManager Instance { get; private set; }

    private Dictionary<string, bool> puzzleCompletion = new();

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
        DontDestroyOnLoad(gameObject);
    }

    public void PuzzleComplete(string id)
    {
        if (!puzzleCompletion.ContainsKey(id))
        {
            puzzleCompletion.TryAdd(id, true);
            //puzzleCompletion[id] = true;
            Debug.Log($"Pizzle completed: {id}");
        }
        else
        {
            Debug.Log("Its Already Exists");
        }

        foreach (var kvp in puzzleCompletion)
        {
            Debug.Log($"Puzzle ID: {kvp.Key}, Completed: {kvp.Value}");
        }
    }

    public bool isPuzzleCompleted(string id)
    {
        return puzzleCompletion.TryGetValue(id, out bool complete) && complete;
    }
}
