using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Circle signed distance field: d = length(p - center) - radius.
    /// </summary>
    public class SdfCircle2D : SdfShape2D
    {
        [SerializeField] private Vector2 center = Vector2.zero;
        [SerializeField] private float radius = 2f;

        public Vector2 Center => center;
        public float Radius => radius;

        private void OnValidate()
        {
            radius = Mathf.Max(0.0001f, radius);
        }

        public override float Evaluate(Vector2 point)
        {
            return (point - center).magnitude - radius;
        }
    }
}
