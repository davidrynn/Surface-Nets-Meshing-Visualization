using UnityEngine;

namespace SurfaceNets2D
{
    /// <summary>
    /// Base class for 2D signed distance field shapes.
    /// </summary>
    public abstract class SdfShape2D : MonoBehaviour
    {
        public abstract float Evaluate(Vector2 point);
    }

    public enum SdfShape
    {
        Circle = 0
    }
}
