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
        [SerializeField] private Color gizmoColor = new Color(0.2f, 0.6f, 1f, 0.9f);

        public Vector2 Center => center;
        public float Radius => radius;
        public Color GizmoColor => gizmoColor;

        private void OnValidate()
        {
            radius = Mathf.Max(0.0001f, radius);
        }

        public override float Evaluate(Vector2 point)
        {
            return (point - center).magnitude - radius;
        }

        private void OnDrawGizmosSelected()
        {
            // Keep visualization locked to the XY plane for 2D scenes.
            Gizmos.color = gizmoColor;
            Vector3 center3D = new Vector3(center.x, center.y, 0f);

#if UNITY_EDITOR
            UnityEditor.Handles.color = gizmoColor;
            UnityEditor.Handles.DrawWireDisc(center3D, Vector3.forward, radius);
#else
            Gizmos.DrawWireSphere(center3D, radius);
#endif

            const float crossSize = 0.1f;
            Gizmos.DrawLine(center3D + Vector3.left * crossSize, center3D + Vector3.right * crossSize);
            Gizmos.DrawLine(center3D + Vector3.up * crossSize, center3D + Vector3.down * crossSize);
        }
    }
}
