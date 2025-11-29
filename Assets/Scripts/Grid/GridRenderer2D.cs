using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Renders the base sampling grid using gizmos so it is visible in both edit and play mode.
    /// </summary>
    [ExecuteAlways]
    public class GridRenderer2D : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridConfig config;

        [Header("Appearance")]
        [SerializeField] private Color gridColor = new Color(1f, 1f, 1f, 0.35f);
        [SerializeField] private Color axisColor = new Color(1f, 0.6f, 0.25f, 0.6f);

        private void OnValidate()
        {
            if (config == null)
            {
                config = GetComponent<GridConfig>();
            }
        }

        private void OnDrawGizmos()
        {
            if (config == null)
            {
                return;
            }

            DrawGrid();
        }

        private void DrawGrid()
        {
            Vector3 origin = new Vector3(config.Origin.x, config.Origin.y, config.GridPlaneZ);

            Gizmos.color = gridColor;
            for (int x = 0; x <= config.GridWidth; x++)
            {
                Vector3 start = origin + new Vector3(x * config.CellSize, 0f, 0f);
                Vector3 end = origin + new Vector3(x * config.CellSize, config.GridHeight * config.CellSize, 0f);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= config.GridHeight; y++)
            {
                Vector3 start = origin + new Vector3(0f, y * config.CellSize, 0f);
                Vector3 end = origin + new Vector3(config.GridWidth * config.CellSize, y * config.CellSize, 0f);
                Gizmos.DrawLine(start, end);
            }

            Gizmos.color = axisColor;
            Gizmos.DrawLine(origin, origin + new Vector3(config.GridWidth * config.CellSize, 0f, 0f));
            Gizmos.DrawLine(origin, origin + new Vector3(0f, config.GridHeight * config.CellSize, 0f));
        }
    }
}
