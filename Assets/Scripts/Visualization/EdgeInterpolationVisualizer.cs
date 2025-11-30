using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Demonstrates zero-crossing interpolation along a single grid edge.
    /// Draws endpoint samples, computes the interpolated surface point, and animates a marker sliding to it.
    /// </summary>
    [ExecuteAlways]
    public class EdgeInterpolationVisualizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridConfig config;
        [SerializeField] private SdfFieldVisualizer sdfField;
        [SerializeField] private SamplePointVisualizer samplePointVisualizer;

        [Header("Edge Selection")]
        [SerializeField] private Vector2Int startSample = new Vector2Int(3, 3);
        [SerializeField] private Vector2Int endSample = new Vector2Int(4, 3);
        [SerializeField] private float edgeZOffset = 0.002f;

        [Header("Appearance")]
        [SerializeField] private float endpointRadius = 0.06f;
        [SerializeField] private float markerRadius = 0.08f;
        [SerializeField] private Color edgeColor = new Color(1f, 0.85f, 0f, 0.7f);
        [SerializeField] private Color markerColor = new Color(0.2f, 0.75f, 1f, 0.95f);

        [Header("Interpolation")]
        [SerializeField] private float signEpsilon = 0.0001f;
        [SerializeField] private bool animateMarker = true;
        [SerializeField] private bool autoRestartOnEnable = true;
        [SerializeField] private bool triggerAnimation = false;
        [SerializeField] private float animationDuration = 1.5f;
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float animationStartTime = -1f;

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

            if (samplePointVisualizer == null)
            {
                samplePointVisualizer = GetComponent<SamplePointVisualizer>();
            }

            edgeZOffset = Mathf.Max(0f, edgeZOffset);
            endpointRadius = Mathf.Max(0.001f, endpointRadius);
            markerRadius = Mathf.Max(endpointRadius, markerRadius);
            signEpsilon = Mathf.Max(0f, signEpsilon);
            animationDuration = Mathf.Max(0.01f, animationDuration);

            if (triggerAnimation)
            {
                RestartAnimation();
                triggerAnimation = false;
            }
        }

        private void OnEnable()
        {
            if (autoRestartOnEnable)
            {
                RestartAnimation();
            }
        }

        private void OnDrawGizmos()
        {
            if (!TryGetEdgeData(out Vector3 start, out Vector3 end, out float d1, out float d2))
            {
                return;
            }

            bool hasSignChange = HasSignChange(d1, d2);
            Vector3 zOffset = new Vector3(0f, 0f, edgeZOffset);

            DrawEdge(start + zOffset, end + zOffset);
            DrawEndpoint(start + zOffset, d1);
            DrawEndpoint(end + zOffset, d2);

            if (!hasSignChange)
            {
                return;
            }

            if (Mathf.Approximately(d1, d2))
            {
                return;
            }

            float t = d1 / (d1 - d2);
            float displayedT = GetAnimatedT(t);
            displayedT = Mathf.Clamp01(displayedT);
            t = Mathf.Clamp01(t);

            Vector3 zeroCrossing = Vector3.Lerp(start, end, t) + zOffset;
            Vector3 animatedMarker = Vector3.Lerp(start, end, displayedT) + zOffset;

            DrawZeroCrossing(zeroCrossing);
            DrawMarker(animatedMarker);

#if UNITY_EDITOR
            if (!Application.isPlaying && animateMarker)
            {
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }

        [ContextMenu("Restart Animation")]
        public void RestartAnimation()
        {
            animationStartTime = Time.realtimeSinceStartup;
        }

        private bool TryGetEdgeData(out Vector3 start, out Vector3 end, out float d1, out float d2)
        {
            start = Vector3.zero;
            end = Vector3.zero;
            d1 = 0f;
            d2 = 0f;

            if (config == null || sdfField == null)
            {
                return false;
            }

            if (!IsWithinGrid(startSample) || !IsWithinGrid(endSample))
            {
                return false;
            }

            EnsureSdfSamples();

            start = config.GetPointPosition(startSample.x, startSample.y);
            end = config.GetPointPosition(endSample.x, endSample.y);
            d1 = sdfField.GetSdfValue(startSample.x, startSample.y);
            d2 = sdfField.GetSdfValue(endSample.x, endSample.y);

            return true;
        }

        private bool IsWithinGrid(Vector2Int sample)
        {
            return sample.x >= 0
                && sample.y >= 0
                && sample.x <= config.GridWidth
                && sample.y <= config.GridHeight;
        }

        private bool HasSignChange(float a, float b)
        {
            int signA = GetSignCategory(a);
            int signB = GetSignCategory(b);

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

        private float GetAnimatedT(float targetT)
        {
            if (!animateMarker)
            {
                return targetT;
            }

            if (animationStartTime < 0f)
            {
                RestartAnimation();
            }

            float elapsed = Time.realtimeSinceStartup - animationStartTime;
            float normalized = Mathf.Clamp01(elapsed / animationDuration);
            float curved = animationCurve != null ? animationCurve.Evaluate(normalized) : normalized;

            return Mathf.Lerp(0f, targetT, curved);
        }

        private void DrawEdge(Vector3 start, Vector3 end)
        {
            Gizmos.color = edgeColor;
            Gizmos.DrawLine(start, end);
        }

        private void DrawEndpoint(Vector3 position, float sdfValue)
        {
            Gizmos.color = GetEndpointColor(sdfValue);
            Gizmos.DrawSphere(position, endpointRadius);
        }

        private Color GetEndpointColor(float sdfValue)
        {
            if (samplePointVisualizer != null)
            {
                if (sdfValue > signEpsilon)
                {
                    return samplePointVisualizer.GetOutsideColor();
                }

                if (sdfValue < -signEpsilon)
                {
                    return samplePointVisualizer.GetInsideColor();
                }
            }

            return sdfValue >= 0f ? Color.red : Color.green;
        }

        private void DrawZeroCrossing(Vector3 position)
        {
            Gizmos.color = edgeColor;
            Gizmos.DrawSphere(position, endpointRadius * 0.9f);
        }

        private void DrawMarker(Vector3 position)
        {
            Gizmos.color = markerColor;
            Gizmos.DrawSphere(position, markerRadius);
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
    }
}
