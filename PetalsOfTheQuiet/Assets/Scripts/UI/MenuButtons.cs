using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    

    public void Play()
    {
        if (TransitionController.Instance.isTransition) return;

        Debug.Log("Play Game");
        TransitionController.Instance.NextScene("Main");
    }

    public void Quit()
    {   
        if (TransitionController.Instance.isTransition) return;

        Debug.Log("Quit Game");
        Application.Quit();
    }
}
