using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private bool IsPaused = false;


    void Start()
    {
        PausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.isEndingMoment)
        {
            if (!IsPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PausePanel.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0;
    }

    void UnpauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PausePanel.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1;
    }

    void UnpauseExit()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        UnpauseGame();
    }

    public void Exit()
    {
        UnpauseExit();
        TransitionController.Instance.NextScene("Menu");
        //SceneManager.LoadSceneAsync("Menu");
    }
}
