using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        Debug.Log("Play Game");
        //SceneManager.LoadScene("Main");
        SceneManager.LoadSceneAsync("Main");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
