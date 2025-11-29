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
        [SerializeField] private SdfFieldVisualizer sdfField;

        [Header("Colors")]
        [SerializeField] private Color defaultPointColor = new Color(0.8f, 0.9f, 1f, 0.9f);
        [SerializeField] private Color insideColor = new Color(0.2f, 0.8f, 0.35f, 0.95f);
        [SerializeField] private Color outsideColor = new Color(1f, 0.35f, 0.35f, 0.95f);
        [SerializeField] private Color zeroColor = new Color(1f, 0.9f, 0.3f, 0.95f);
        [SerializeField] private Color signChangeColor = new Color(1f, 0.85f, 0f, 0.9f);

        [Header("Shape")]
        [SerializeField] private float pointRadius = 0.05f;

        [Header("Sign Visualization")]
        [SerializeField] private bool visualizeSigns = true;
        [SerializeField] private bool drawSignChangeEdges = true;
        [SerializeField] private float signEpsilon = 0.0001f;
        [SerializeField] private float edgeZOffset = 0.001f;

        private void OnValidate()
        {
            if (config == null)
            {
                config = GetComponent<GridConfig>();
            }

            if (sdfField == null)
            {
                sdfField = GetComponent<SdfFieldVisualizer>();
            }

            pointRadius = Mathf.Max(0.001f, pointRadius);
            signEpsilon = Mathf.Max(0f, signEpsilon);
            edgeZOffset = Mathf.Max(0f, edgeZOffset);
        }

        private void OnDrawGizmos()
        {
            if (config == null)
            {
                return;
            }

            EnsureSdfSamples();
            DrawSamplePoints();
            DrawSignChangeEdges();
        }

        private void DrawSamplePoints()
        {
            for (int y = 0; y <= config.GridHeight; y++)
            {
                for (int x = 0; x <= config.GridWidth; x++)
                {
                    Gizmos.color = GetPointColor(x, y);
                    Vector3 position = config.GetPointPosition(x, y);
                    Gizmos.DrawSphere(position, pointRadius);
                }
            }
        }

        private void DrawSignChangeEdges()
        {
            if (!drawSignChangeEdges || sdfField == null || sdfField.SdfValues == null)
            {
                return;
            }

            Gizmos.color = signChangeColor;
            Vector3 zOffset = new Vector3(0f, 0f, edgeZOffset);

            for (int y = 0; y <= config.GridHeight; y++)
            {
                for (int x = 0; x < config.GridWidth; x++)
                {
                    if (!HasSignChange(x, y, x + 1, y))
                    {
                        continue;
                    }

                    Vector3 start = config.GetPointPosition(x, y) + zOffset;
                    Vector3 end = config.GetPointPosition(x + 1, y) + zOffset;
                    Gizmos.DrawLine(start, end);
                }
            }

            for (int y = 0; y < config.GridHeight; y++)
            {
                for (int x = 0; x <= config.GridWidth; x++)
                {
                    if (!HasSignChange(x, y, x, y + 1))
                    {
                        continue;
                    }

                    Vector3 start = config.GetPointPosition(x, y) + zOffset;
                    Vector3 end = config.GetPointPosition(x, y + 1) + zOffset;
                    Gizmos.DrawLine(start, end);
                }
            }
        }

        private Color GetPointColor(int x, int y)
        {
            if (!visualizeSigns || sdfField == null || sdfField.SdfValues == null)
            {
                return defaultPointColor;
            }

            float value = sdfField.GetSdfValue(x, y);

            if (value > signEpsilon)
            {
                return outsideColor;
            }

            if (value < -signEpsilon)
            {
                return insideColor;
            }

            return zeroColor;
        }

        private bool HasSignChange(int x1, int y1, int x2, int y2)
        {
            int signA = GetSignCategory(sdfField.GetSdfValue(x1, y1));
            int signB = GetSignCategory(sdfField.GetSdfValue(x2, y2));

            if (signA == 0 && signB == 0)
            {
                return false;
            }

            return signA != signB;
        }

        private int GetSignCategory(float value)
        {
            if (value > signEpsilon)
            {
                return 1;
            }

            if (value < -signEpsilon)
            {
                return -1;
            }

            return 0;
        }

        private void EnsureSdfSamples()
        {
            if (sdfField == null || config == null)
            {
                return;
            }

            int expectedWidth = config.GridWidth + 1;
            int expectedHeight = config.GridHeight + 1;

            bool needsResample = sdfField.SdfValues == null
                || sdfField.SdfValues.GetLength(0) != expectedWidth
                || sdfField.SdfValues.GetLength(1) != expectedHeight;

            if (needsResample)
            {
                sdfField.RecalculateField();
            }
        }

        public Color GetInsideColor() => insideColor;
        public Color GetOutsideColor() => outsideColor;
    }
}
