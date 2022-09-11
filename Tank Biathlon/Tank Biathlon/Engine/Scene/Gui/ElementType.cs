using Microsoft.Xna.Framework;

namespace Iris
{
    public class ElementType
    {
        private Vector2 bounds;
        private float layer_depth;

        public Vector2 Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public float Width
        {
            get { return bounds.X; }
        }

        public float Height
        {
            get { return bounds.Y; }
        }

        public float LayerDepth
        {
            get { return layer_depth; }
            set { layer_depth = value; }
        }
    }
}
