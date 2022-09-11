using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iris
{
    public class TypeButton : ElementType
    {
        private Texture2D texture;
        private SpriteFont font;

        public TypeButton(Texture2D texture, SpriteFont font, float width, float height)
        {
            this.texture = texture;
            this.font = font;
            Bounds = new Vector2(width, height);
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
    }
}
