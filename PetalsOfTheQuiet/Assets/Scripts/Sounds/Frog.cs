using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float scareDistance = 3f; 
    [SerializeField] private float resumeDistance = 5f; 

    private AudioSource audioSource;
    private bool isScared = false;

    //private bool firstTime = false; -> If i will have a voice.

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isScared && distance < scareDistance)
        {
            audioSource.Pause();
            isScared = true;
        }
        else if (isScared && distance > resumeDistance)
        {
            audioSource.Play();
            isScared = false;
        }
    }
}
