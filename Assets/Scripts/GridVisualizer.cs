using UnityEngine;

/// <summary>
/// Draws a simple 2D grid using gizmos to act as the base visual for the surface nets steps.
/// </summary>
[ExecuteAlways]
public class GridVisualizer : MonoBehaviour
{
    [Header("Grid Dimensions")]
    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector2 origin = Vector2.zero;

    [Header("Appearance")]
    [SerializeField] private float gridPlaneZ = 0f;
    [SerializeField] private Color gridColor = new Color(1f, 1f, 1f, 0.35f);
    [SerializeField] private Color axisColor = new Color(1f, 0.6f, 0.25f, 0.6f);

    private void OnValidate()
    {
        gridWidth = Mathf.Max(1, gridWidth);
        gridHeight = Mathf.Max(1, gridHeight);
        cellSize = Mathf.Max(0.01f, cellSize);
    }

    private void OnDrawGizmos()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        Vector3 origin3 = new Vector3(origin.x, origin.y, gridPlaneZ);
        Gizmos.color = gridColor;

        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = origin3 + new Vector3(x * cellSize, 0f, 0f);
            Vector3 end = origin3 + new Vector3(x * cellSize, gridHeight * cellSize, 0f);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= gridHeight; y++)
        {
            Vector3 start = origin3 + new Vector3(0f, y * cellSize, 0f);
            Vector3 end = origin3 + new Vector3(gridWidth * cellSize, y * cellSize, 0f);
            Gizmos.DrawLine(start, end);
        }

        Gizmos.color = axisColor;
        Gizmos.DrawLine(origin3, origin3 + new Vector3(gridWidth * cellSize, 0f, 0f));
        Gizmos.DrawLine(origin3, origin3 + new Vector3(0f, gridHeight * cellSize, 0f));
    }
}
