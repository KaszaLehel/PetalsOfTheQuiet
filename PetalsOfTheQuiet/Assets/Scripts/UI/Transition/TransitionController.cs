using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance { get; private set; }
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject canvas;

    public bool isTransition = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        canvas.SetActive(false);
    }

    public void NextScene(string sceneName)
    {
        isTransition = true;
        canvas.SetActive(true);
        StartCoroutine(LevelChange(sceneName));
        //SceneManager.LoadSceneAsync(index);
    }

    IEnumerator LevelChange(string name)
    {
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);  //nem lehet Async mert akkor nem biztos hogy előbb betölti mint ahogy végig fut a Coroutine.
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        canvas.SetActive(false);
        isTransition = false;
    }
}
