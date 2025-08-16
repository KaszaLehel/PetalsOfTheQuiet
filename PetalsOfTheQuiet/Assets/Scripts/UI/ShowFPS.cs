using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public TMP_Text fpsText;
    float deltaTime = 0f;

    void Update()
    {
        if (fpsText != null)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            int fps = (int)(1f / deltaTime);
            fpsText.text = "FPS: " + fps; 
        }
        
    }
}
