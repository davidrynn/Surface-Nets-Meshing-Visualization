using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Centralized configuration for the sampling grid used across the visualization steps.
    /// </summary>
    [ExecuteAlways]
    public class GridConfig : MonoBehaviour
    {
        [Header("Grid Dimensions")]
        [SerializeField] private int gridWidth = 8;
        [SerializeField] private int gridHeight = 8;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector2 origin = Vector2.zero;

        [Header("Plane Settings")]
        [SerializeField] private float gridPlaneZ = 0f;

        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public float CellSize => cellSize;
        public Vector2 Origin => origin;
        public float GridPlaneZ => gridPlaneZ;

        private void OnValidate()
        {
            gridWidth = Mathf.Max(1, gridWidth);
            gridHeight = Mathf.Max(1, gridHeight);
            cellSize = Mathf.Max(0.01f, cellSize);
        }

        public Vector3 GetPointPosition(int x, int y)
        {
            return new Vector3(origin.x + x * cellSize, origin.y + y * cellSize, gridPlaneZ);
        }
    }
}
