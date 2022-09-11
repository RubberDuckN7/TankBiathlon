using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iris
{
    public class Entity : Element
    {
        private Texture2D texture;
        private Rectangle bounds;
        private float layer_depth;

        public Entity(Texture2D texture, Rectangle bounds, float layer_depth)
        {
            this.texture = texture;
            this.bounds = bounds;
            this.layer_depth = layer_depth;
        }

        //gs2d.SP.Draw(t_tank, tank, null, Color.White, MathHelper.ToRadians(rotation),
        //    new Vector2(t_tank.Width / 2, t_tank.Height / 2), SpriteEffects.None, 1.0f);

        public void Draw(Graphics2D gs2d, float rotation, float depth)
        {
            Draw(gs2d, Vector2.Zero, Color.White, rotation, depth);
        }

        public void Draw(Graphics2D gs2d, Color color, float rotation, float depth)
        {
            Draw(gs2d, Vector2.Zero, color, rotation, depth);
        }

        public void Draw(Graphics2D gs2d, Vector2 offset, float rotation, float depth)
        {
            Draw(gs2d, offset, Color.White, rotation, depth);
        }

        public void Draw(Graphics2D gs2d, Vector2 offset, Color color, float rotation, float depth)
        {
            int x = bounds.X;
            int y = bounds.Y;

            bounds.X += (int)(bounds.Width / 2);
            bounds.Y += (int)(bounds.Height / 2);

            gs2d.SP.Draw(texture, bounds, null, Color.White, MathHelper.ToRadians(rotation),
                new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, depth);

            bounds.X = x;
            bounds.Y = y;         
        }

        public override void Draw(Graphics2D gs2d)
        {
            Draw(gs2d, Color.White);
        }

        public override void Draw(Graphics2D gs2d, Color color) 
        {
            Draw(gs2d, color, Vector2.Zero);
        }
        public override void Draw(Graphics2D gs2d, Color color, Vector2 offset)
        {
            int x = bounds.X;
            int y = bounds.Y;

            bounds.X += (int)offset.X;
            bounds.Y += (int)offset.Y;

            gs2d.Draw(texture, bounds, color, layer_depth);
            //sp.Draw(texture, bounds, null, color, 0f, Vector2.Zero, SpriteEffects.None, layer_depth);

            bounds.X = x;
            bounds.Y = y;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public void SetX(float x)
        {
            bounds.X = (int)x;
        }

        public void SetY(float y)
        {
            bounds.Y = (int)y;
        }

        public int X
        {
            get { return bounds.X; }
        }

        public int Y
        {
            get { return bounds.Y; }
        }

        public int Width
        {
            get { return bounds.Width; }
        }

        public int Height
        {
            get { return bounds.Height; }
        }

        public float LayerDepth
        {
            get { return layer_depth; }
            set { layer_depth = value; }
        }
    }
}
