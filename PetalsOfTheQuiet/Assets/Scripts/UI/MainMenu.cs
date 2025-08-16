using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject volmeSettings;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowButtons();
    }

    public void ShowButtons()
    {
        menuButtons.SetActive(true);
        credits.SetActive(false);
        volmeSettings.SetActive(false);
    }

    public void ShowSettings()
    {
        menuButtons.SetActive(false);
        credits.SetActive(false);
        volmeSettings.SetActive(true);
    }

    public void ShowCredits()
    {
        menuButtons.SetActive(false);
        credits.SetActive(true);
        volmeSettings.SetActive(false);
    }
}
