using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{
    [Header("Base Wind")]
    [Tooltip("Base wind animate the trunks")]
    [Range(0f, 5f)]
    public float baseWindPower = 3f;
    [Tooltip("Base wind animate the trunks")]
    public float baseWindSpeed = 1f;

    [Header("Wind Burst")]
    [Tooltip("Bursts are managed by a moving World-Space noise that multiply the base wind speed and power")]
    [Range(0f, 10f)]
    public float burstsPower = 0.5f;
    [Tooltip("Speed of the Bursts noise")]
    public float burstsSpeed = 5f;
    [Tooltip("Size of the Bursts noise in Word-Space")]
    public float burstsScale = 10f;

    [Header("Micro Wind")]
    [Tooltip("Micro wind animate the leaves")]
    [Range(0f, 1f)]
    public float microPower = 0.1f;
    [Tooltip("Micro wind animate the leaves")]
    public float microSpeed = 1f;
    [Tooltip("Micro wind animate the leaves")]
    public float microFrequency = 3f;

    [Space(10)]
    public float renderDistance = 30f;

    void Update()
    {
        UpdateEnvironment();
    }

    private void UpdateEnvironment()
    {
        Shader.SetGlobalFloat("WindPower", baseWindPower);
        Shader.SetGlobalFloat("WindSpeed", baseWindSpeed);
        Shader.SetGlobalFloat("WindBurstsPower", burstsPower);
        Shader.SetGlobalFloat("WindBurstsSpeed", burstsSpeed);
        Shader.SetGlobalFloat("WindBurstsScale", burstsScale);
        Shader.SetGlobalFloat("MicroPower", microPower);
        Shader.SetGlobalFloat("MicroSpeed", microSpeed);
        Shader.SetGlobalFloat("MicroFrequency", microFrequency);
        Shader.SetGlobalFloat("GrassRenderDist", renderDistance);
    }
}
