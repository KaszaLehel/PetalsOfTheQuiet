using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        if (TransitionController.Instance.isTransition) return;

        Debug.Log("Play Game");
        TransitionController.Instance.NextScene("Main");
        //SceneManager.LoadSceneAsync("Main");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
