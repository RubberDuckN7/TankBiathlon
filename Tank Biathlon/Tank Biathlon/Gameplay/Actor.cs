using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Iris;

namespace Tank_Biathlon
{
    public class Actor
    {
        public Rectangle bounds;
        public Texture2D texture;
        public Vector2 pos;

        public Actor()
        {
            bounds = new Rectangle();
            texture = null;
            pos = new Vector2(0f, 0f);
        }
    }
}
