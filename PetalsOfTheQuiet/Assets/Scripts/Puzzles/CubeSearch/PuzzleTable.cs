using UnityEngine;

public class PuzzleTable : MonoBehaviour
{

    [SerializeField] private string expectedCubeID;

    [SerializeField] private GameObject expectedCube;
    [SerializeField] private AudioClip metalSoundFX;
    private FirstPuzzle parentPuzzle;
    private bool isCorrectlyFilled = false;

    private void Start()
    {
        parentPuzzle = GetComponentInParent<FirstPuzzle>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == expectedCube)
        {
            isCorrectlyFilled = true;
            SoundEffectManager.Instance.PlaySoundFX(metalSoundFX, transform, 1f);
            parentPuzzle.CheckSolved();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject == expectedCube)
        {
            isCorrectlyFilled = false;
            parentPuzzle.CheckSolved();
        }
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        PuzzleCubes cube = other.GetComponent<PuzzleCubes>();
        if (cube != null && cube.cubeID == expectedCubeID)
        {
            isCorrectlyFilled = true;
            parentPuzzle.CheckSolved();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PuzzleCubes cube = other.GetComponent<PuzzleCubes>();
        if (cube != null && cube.cubeID == expectedCubeID)
        {
            isCorrectlyFilled = false;
            parentPuzzle.CheckSolved();
        }
    }
*/
    public bool IsFilledCorrectly() => isCorrectlyFilled;
}
