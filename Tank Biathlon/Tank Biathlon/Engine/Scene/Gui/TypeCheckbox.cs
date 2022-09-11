using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iris
{
    public class TypeCheckBox : ElementType
    {
        private Texture2D t_background;
        private Texture2D t_check;

        public TypeCheckBox(Texture2D t_background, Texture2D t_check, float width, float height)
        {
            this.t_background = t_background;
            this.t_check = t_check;
            Bounds = new Vector2(width, height);
        }

        public Texture2D TextureBackground
        {
            get { return t_background; }
            set { t_background = value; }
        }

        public Texture2D TextureCheck
        {
            get { return t_check; }
            set { t_check = value; }
        }
    }
}
