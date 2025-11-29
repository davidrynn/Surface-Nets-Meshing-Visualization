using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Draws sample points at each grid intersection and exposes color hooks for later SDF sign visualization.
    /// </summary>
    [ExecuteAlways]
    public class SamplePointVisualizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridConfig config;

        [Header("Colors")]
        [SerializeField] private Color defaultPointColor = new Color(0.8f, 0.9f, 1f, 0.9f);
        [SerializeField] private Color insideColor = new Color(0.2f, 0.8f, 0.35f, 0.95f);
        [SerializeField] private Color outsideColor = new Color(1f, 0.35f, 0.35f, 0.95f);

        [Header("Shape")]
        [SerializeField] private float pointRadius = 0.05f;

        private void OnValidate()
        {
            if (config == null)
            {
                config = GetComponent<GridConfig>();
            }

            pointRadius = Mathf.Max(0.001f, pointRadius);
        }

        private void OnDrawGizmos()
        {
            if (config == null)
            {
                return;
            }

            DrawSamplePoints();
        }

        private void DrawSamplePoints()
        {
            Gizmos.color = defaultPointColor;

            for (int y = 0; y <= config.GridHeight; y++)
            {
                for (int x = 0; x <= config.GridWidth; x++)
                {
                    Vector3 position = config.GetPointPosition(x, y);
                    Gizmos.DrawSphere(position, pointRadius);
                }
            }
        }

        public Color GetInsideColor() => insideColor;
        public Color GetOutsideColor() => outsideColor;
    }
}
