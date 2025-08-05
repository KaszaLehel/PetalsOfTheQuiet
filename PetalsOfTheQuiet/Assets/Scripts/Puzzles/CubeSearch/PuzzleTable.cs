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
            parentPuzzle.CheckSolved();

            if(GameManager.Instance.isSoundOn)
                SoundEffectManager.Instance.PlaySoundFX(metalSoundFX, transform, 1f);
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
