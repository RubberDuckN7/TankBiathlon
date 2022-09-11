using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Iris
{
    public class Graphics2D
    {
        private SpriteBatch sp;

        public Graphics2D(SpriteBatch sp)
        {
            this.sp = sp;
        }

        public void Begin()
        {
            sp.Begin();
        }

        public void End()
        {
            sp.End();
        }

        public void Draw(Texture2D texture, Rectangle bounds)
        {
            DrawFull(texture, bounds, Color.White, SpriteEffects.None, Vector2.Zero, 0.0f, 1.0f);
        }

        public void Draw(Texture2D texture, Rectangle bounds, Color color)
        {
            DrawFull(texture, bounds, color, SpriteEffects.None, Vector2.Zero, 0.0f, 1.0f);
        }

        public void Draw(Texture2D texture, Rectangle bounds, Color color, float depth)
        {
            DrawFull(texture, bounds, color, SpriteEffects.None, Vector2.Zero, 0.0f, depth);
        }

        public void Draw(Texture2D texture, Rectangle bounds, Color color, Vector2 origin, float rotation, float depth)
        {
            DrawFull(texture, bounds, color, SpriteEffects.None, origin, rotation, depth);
        }

        public void Draw(Texture2D texture, Rectangle bounds, Color color, SpriteEffects effects, float rotation, float depth)
        {
            DrawFull(texture, bounds, color, effects, Vector2.Zero, rotation, depth);
        }

        public SpriteBatch SP
        {
            get { return sp; }
        }

        private void DrawFull(Texture2D texture, Rectangle bounds, Color color, SpriteEffects effects, Vector2 origin, float rotation, float depth)
        {
            sp.Draw(texture, bounds, null, color, rotation, Vector2.Zero, effects, depth);
        }
    }
}
