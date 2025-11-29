using System.Collections.Generic;
using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Visualizes SDF distance vectors for selected grid sample points.
    /// Arrows point toward the implicit surface with an optional pulse animation.
    /// </summary>
    [ExecuteAlways]
    public class SdfVectorVisualizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridConfig config;
        [SerializeField] private SdfFieldVisualizer sdfField;

        [Header("Targets")]
        [SerializeField] private bool drawAllSamples = false;
        [SerializeField] private Vector2Int[] highlightedSamples;

        [Header("Appearance")]
        [SerializeField] private Color insideVectorColor = new Color(0.2f, 0.8f, 0.35f, 0.95f);
        [SerializeField] private Color outsideVectorColor = new Color(1f, 0.35f, 0.35f, 0.95f);
        [SerializeField] private Color neutralVectorColor = new Color(0.25f, 0.75f, 1f, 0.95f);
        [SerializeField] private float vectorZOffset = 0.003f;
        [SerializeField] private float arrowHeadLength = 0.1f;
        [SerializeField] private float arrowHeadAngle = 20f;

        [Header("Animation")]
        [SerializeField] private bool animateVectors = true;
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField] private float minimumScale = 0.2f;
        [SerializeField] private float signEpsilon = 0.0001f;

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

            vectorZOffset = Mathf.Max(0f, vectorZOffset);
            arrowHeadLength = Mathf.Max(0.001f, arrowHeadLength);
            animationSpeed = Mathf.Max(0f, animationSpeed);
            minimumScale = Mathf.Clamp01(minimumScale);
            signEpsilon = Mathf.Max(0f, signEpsilon);
        }

        private void OnDrawGizmos()
        {
            if (config == null || sdfField == null)
            {
                return;
            }

            EnsureSdfSamples();
            DrawDistanceVectors();
        }

        private void EnsureSdfSamples()
        {
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

        private void DrawDistanceVectors()
        {
            Vector3 zOffset = new Vector3(0f, 0f, vectorZOffset);
            float animationScale = GetAnimationScale();

            foreach (Vector2Int sample in EnumerateRelevantSamples())
            {
                float sdfValue = sdfField.GetSdfValue(sample.x, sample.y);
                Vector3 start = config.GetPointPosition(sample.x, sample.y) + zOffset;
                Vector3 vectorToSurface = sdfField.GetDistanceVectorToSurface(start, sdfValue) * animationScale;

                if (vectorToSurface.sqrMagnitude <= Mathf.Epsilon)
                {
                    continue;
                }

                Gizmos.color = GetVectorColor(sdfValue);
                DrawArrow(start, vectorToSurface);
            }
        }

        private IEnumerable<Vector2Int> EnumerateRelevantSamples()
        {
            foreach (Vector2Int sample in EnumerateSelectedSamples())
            {
                if (IsOnSignChange(sample.x, sample.y))
                {
                    yield return sample;
                }
            }
        }

        private IEnumerable<Vector2Int> EnumerateSelectedSamples()
        {
            if (drawAllSamples || highlightedSamples == null || highlightedSamples.Length == 0)
            {
                for (int y = 0; y <= config.GridHeight; y++)
                {
                    for (int x = 0; x <= config.GridWidth; x++)
                    {
                        yield return new Vector2Int(x, y);
                    }
                }

                yield break;
            }

            foreach (Vector2Int sample in highlightedSamples)
            {
                if (sample.x < 0 || sample.y < 0 || sample.x > config.GridWidth || sample.y > config.GridHeight)
                {
                    continue;
                }

                yield return sample;
            }
        }

        private bool IsOnSignChange(int x, int y)
        {
            int sign = GetSignCategory(sdfField.GetSdfValue(x, y));

            bool HasNeighborSignChange(int nx, int ny)
            {
                if (nx < 0 || ny < 0 || nx > config.GridWidth || ny > config.GridHeight)
                {
                    return false;
                }

                int neighborSign = GetSignCategory(sdfField.GetSdfValue(nx, ny));

                if (neighborSign == 0 && sign == 0)
                {
                    return false;
                }

                return neighborSign != sign;
            }

            return HasNeighborSignChange(x + 1, y)
                || HasNeighborSignChange(x - 1, y)
                || HasNeighborSignChange(x, y + 1)
                || HasNeighborSignChange(x, y - 1);
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

        private void DrawArrow(Vector3 start, Vector3 vector)
        {
            Vector3 end = start + vector;
            Gizmos.DrawLine(start, end);

            Vector3 direction = vector.normalized;
            Vector3 right = Quaternion.AngleAxis(arrowHeadAngle, Vector3.forward) * -direction;
            Vector3 left = Quaternion.AngleAxis(-arrowHeadAngle, Vector3.forward) * -direction;

            Gizmos.DrawLine(end, end + right * arrowHeadLength);
            Gizmos.DrawLine(end, end + left * arrowHeadLength);
        }

        private Color GetVectorColor(float sdfValue)
        {
            if (sdfValue > 0f)
            {
                return outsideVectorColor;
            }

            if (sdfValue < 0f)
            {
                return insideVectorColor;
            }

            return neutralVectorColor;
        }

        private float GetAnimationScale()
        {
            if (!animateVectors)
            {
                return 1f;
            }

            float pulse = 0.5f * (Mathf.Sin(Time.realtimeSinceStartup * animationSpeed) + 1f);
            return Mathf.Lerp(minimumScale, 1f, pulse);
        }
    }
}
