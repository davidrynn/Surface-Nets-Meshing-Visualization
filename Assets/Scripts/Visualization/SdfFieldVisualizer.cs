using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Samples a signed distance field across the grid and stores the values for later visualization steps.
    /// </summary>
    [ExecuteAlways]
    public class SdfFieldVisualizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridConfig config;
        [SerializeField] private SdfShape selectedShape = SdfShape.Circle;
        [SerializeField] private SdfCircle2D circle;

        [Header("Sampling")]
        [SerializeField] private bool autoUpdate = true;

        private float[,] sdfValues;

        public float[,] SdfValues => sdfValues;
        public SdfShape SelectedShape => selectedShape;
        public SdfCircle2D Circle => circle;

        private void OnValidate()
        {
            if (config == null)
            {
                config = GetComponent<GridConfig>();
            }

            if (autoUpdate)
            {
                RecalculateField();
            }
        }

        private void OnEnable()
        {
            if (autoUpdate)
            {
                RecalculateField();
            }
        }

        private void Update()
        {
            if (Application.isPlaying && autoUpdate)
            {
                RecalculateField();
            }
        }

        public void RecalculateField()
        {
            if (config == null)
            {
                return;
            }

            int width = config.GridWidth + 1;
            int height = config.GridHeight + 1;

            if (sdfValues == null || sdfValues.GetLength(0) != width || sdfValues.GetLength(1) != height)
            {
                sdfValues = new float[width, height];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 position = config.GetPointPosition(x, y);
                    sdfValues[x, y] = SampleShape(position);
                }
            }
        }

        public float GetSdfValue(int x, int y)
        {
            if (sdfValues == null)
            {
                return 0f;
            }

            int width = sdfValues.GetLength(0);
            int height = sdfValues.GetLength(1);

            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return 0f;
            }

            return sdfValues[x, y];
        }

        private float SampleShape(Vector3 worldPosition)
        {
            Vector2 point2D = new Vector2(worldPosition.x, worldPosition.y);

            switch (selectedShape)
            {
                case SdfShape.Circle:
                    return circle != null ? circle.Evaluate(point2D) : 0f;
                default:
                    return 0f;
            }
        }

        public Vector3 GetDistanceVectorToSurface(Vector3 worldPosition, float sdfValue)
        {
            Vector2 point2D = new Vector2(worldPosition.x, worldPosition.y);

            switch (selectedShape)
            {
                case SdfShape.Circle:
                    if (circle == null)
                    {
                        return Vector3.zero;
                    }

                    Vector2 vector2D = circle.GetVectorToSurface(point2D, sdfValue);
                    return new Vector3(vector2D.x, vector2D.y, 0f);
                default:
                    return Vector3.zero;
            }
        }
    }
}
