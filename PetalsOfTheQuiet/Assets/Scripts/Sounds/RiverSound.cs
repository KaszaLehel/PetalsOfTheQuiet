using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class RiverSound : MonoBehaviour
{
    [Header("River Cart and Path Settings")]

    [Tooltip("Player GameObject")]
    [SerializeField] private Transform player;

    [Tooltip("Spline Container to follow")]
    [SerializeField] private SplineContainer splineContainer;

    void Update()
    {
        if (splineContainer == null || player == null) return;

        Spline spline = splineContainer.Spline;

        // Ray a játékos pozíciójából lefelé
        Ray ray = new Ray(player.position, Vector3.up);

        float t;
        float3 nearestPos;

        t = SplineUtility.GetNearestPoint(spline, ray, out nearestPos, out t);

        // Állítsuk be a pozíciót
        transform.position = nearestPos;

        // Forgatás spline mentén - ehhez pl. tangentet kérhetsz le
        /*float3 tangent = spline.EvaluateTangent(t);
        if (!math.all(tangent == float3.zero))
        {
            transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
        }*/
    }
}
