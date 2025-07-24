using System.Collections.Generic;
using UnityEngine;

/*[RequireComponent(typeof(LineRenderer))]*/
public class PaintableSurface : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private float pointSpacing = 0.1f;

    private LineRenderer currentLine;
    private List<Vector3> currentPoints = new List<Vector3>();
    private List<GameObject> allLines = new List<GameObject>();

    private Vector3 lastPoint;

    public void BeginLine(Vector3 startPoint)
    {
        GameObject newLine = Instantiate(linePrefab, transform);
        currentLine = newLine.GetComponent<LineRenderer>();
        currentPoints.Clear();
        allLines.Add(newLine);

        currentPoints.Add(startPoint);
        currentPoints.Add(startPoint);
        currentLine.positionCount = 2;
        currentLine.SetPosition(0, startPoint);
        currentLine.SetPosition(1, startPoint);
        lastPoint = startPoint;
    }

    public void PaintAt(Vector3 point)
    {
        if (currentLine == null) return;

        if (Vector3.Distance(lastPoint, point) > pointSpacing)
        {
            currentPoints.Add(point);
            currentLine.positionCount = currentPoints.Count;
            currentLine.SetPosition(currentPoints.Count - 1, point);
            lastPoint = point;
        }
    }

    public void EndLine()
    {
        currentLine = null;
        currentPoints.Clear();
    }

    public void ClearDrawing()
    {
        foreach (GameObject line in allLines)
        {
            Destroy(line);
        }
        allLines.Clear();
    }
}
