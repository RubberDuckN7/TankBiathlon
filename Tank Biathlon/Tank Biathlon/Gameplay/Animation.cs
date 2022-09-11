using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Iris;

namespace Tank_Biathlon
{
    public class Animation
    {
        private Texture2D[] textures;
        private Rectangle bounds;
        private float time;
        private float rate;
        private int index;
        private bool alive;
        private bool loop;

        public Animation(Texture2D[] textures, int w, int h, float rate, bool loop)
        {
            this.textures = textures;
            this.bounds = new Rectangle(0, 0, w, h);
            this.time = 0.0f;
            this.rate = rate;
            this.index = 0;
            this.alive = false;
            this.loop = loop;
        }

        public void Play(float x, float y)
        {
            if (!alive)
            {
                bounds.X = (int)x;
                bounds.Y = (int)y;
                alive = true;
                Reset();
            }
        }

        public void Draw(Graphics2D gs2d)
        {
            if(alive)
                gs2d.SP.Draw(textures[index], bounds, Color.White);
        }

        public bool Update(float dt)
        {
            time -= dt * rate;
            if (time < 0f)
            {
                if (index < textures.Length - 1)
                {
                    index++;
                    time = 1.0f;
                }
                else
                {
                    if (loop)
                    {
                        index = 0;
                        time = 1.0f;
                    }
                    else
                        alive = false;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            index = 0;
            time = 0.0f;
        }

        public bool IsAlive()
        {
            return time > 0f;
        }

        public Rectangle GetBounds()
        {
            return bounds; 
        }

        public Texture2D GetTexture()
        {
            return textures[index];
        }
    }
}
