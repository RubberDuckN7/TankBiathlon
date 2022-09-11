using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Iris;

namespace Tank_Biathlon
{
    public class Bullet
    {
        private Entity entity;
        private Vector2 dir;
        private Vector2 pos;
        private float speed;
        private float life_time;
        private float time;

        public Bullet(Entity entity, float speed, float life_time)
        {
            this.entity = entity;
            this.speed = speed;
            this.life_time = life_time;
            this.time = life_time+1f;
        }

        public void Fire(Vector2 pos, Vector2 dir)
        {
            //dir = new Vector2(0f, -1f);
            
            if (time > life_time)
            {
                this.pos = pos;
                this.dir = dir;
                this.time = 0.0f;
                entity.SetX(pos.X);
                entity.SetY(pos.Y);
            }
        }

        public void Kill()
        {
            time = life_time + 1f;
        }

        public void Draw(Graphics2D gs2d)
        {
            if (time < life_time)
            {
                entity.Draw(gs2d);
            }
        }

        public void Update(float dt)
        {
            if (time < life_time)
            {
                time += dt;
                float ax = dir.X * speed * dt;
                float ay = dir.Y * speed * dt;
                float x = (float)(entity.X + ax);
                float y = (float)(entity.Y + ay);

                pos.X += dir.X * speed * dt;
                pos.Y += dir.Y * speed * dt;

                entity.SetX(pos.X);
                entity.SetY(pos.Y);
            }
        }

        public Vector2 GetPos()
        {
            return pos; 
        }

        public Rectangle GetBounds()
        {
            return entity.Bounds;
        }

        public bool IsAlive()
        {
            return time < life_time;
        }
    }
}
