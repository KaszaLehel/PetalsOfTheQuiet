using System.Collections;
using System.Timers;
using UnityEngine;
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
        PausePanel.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0;
    }

    void UnpauseGame()
    {
        PausePanel.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1;
    }
}
