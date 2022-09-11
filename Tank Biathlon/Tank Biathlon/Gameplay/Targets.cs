using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Iris;

namespace Tank_Biathlon
{
    public class Targets
    {
        private List<Vector2> instances;
        private Texture2D block_texture;

        private Vector2 screen_bounds;
        private Rectangle block_bounds;

        private GameScene scene;

        public Targets(GameScene scene, Texture2D block_texture, int block_w, int block_h, Vector2 screen_bounds)
        {
            this.scene = scene;
            this.instances = new List<Vector2>();
            this.block_texture = block_texture;
            this.screen_bounds = screen_bounds;
            this.block_bounds = new Rectangle(0, 0, block_w, block_h);

            for (int i = 0; i < 4; i++)
            {
                instances.Add(new Vector2());
                Respawn(i);
            }
        }

        public void Draw(Graphics2D gs2d)
        {
            float shrink = (float)(block_bounds.Width * 0.05f);
            Rectangle b = new Rectangle(0, 0,
                (int)(block_bounds.Width - shrink * 2), (int)(block_bounds.Height - shrink * 2));



            foreach (Vector2 i in instances)
            {
                if (i.Y > 0f)
                {
                    block_bounds.X = (int)i.X;
                    block_bounds.Y = (int)i.Y;

                    gs2d.SP.Draw(block_texture, block_bounds, Color.White);

                    //b.X = (int)(i.X + shrink);
                    //b.Y = (int)(i.Y + shrink);
                    //gs2d.SP.Draw(scene.debug_texture, b, Color.White);
                }
            }
        }

        public void Update(float step)
        {
            for (int i = 0; i < instances.Count; i++)
            {
                Vector2 v2 = instances[i];
                v2.Y += step;
                instances[i] = v2;
                if (v2.Y > screen_bounds.Y)
                    Respawn(i);
            }
        }

        public void Respawn(int id)
        {
            Vector2 v2 = instances[id];
            instances[id] = scene.Spawn();
        }

        public bool Collide(Rectangle tank, Bullet bullet)
        {
            if (!scene.IsAlive())
                return false;

            float shrink = (float)(block_bounds.Width * 0.05f);
            Rectangle b = new Rectangle(0, 0,
                (int)(block_bounds.Width - shrink * 2), (int)(block_bounds.Height - shrink * 6));

            for(int i = 0; i < instances.Count; i++)
            {
                b.X = (int)(instances[i].X + shrink);
                b.Y = (int)(instances[i].Y + shrink);

                if (Collision.RectangleVsRectangle(b, tank))
                {
                    scene.CrashTarget(instances[i].X, instances[i].Y);
                    Respawn(i);
                    return true;
                }

                Rectangle bb = bullet.GetBounds();
                bb.X = (int)bullet.GetPos().X;
                bb.Y = (int)bullet.GetPos().Y;
                if (bullet.IsAlive() && Collision.RectangleVsRectangle(bb, b))
                {
                    bullet.Kill();
                    scene.BulletHit(bullet.GetPos().X, bullet.GetPos().Y);
                    

                    float perc = (float)(block_bounds.Width * 0.3f);

                    if (bb.X + bb.Width > block_bounds.X + perc &&
                        bb.X < block_bounds.X + block_bounds.Width - perc)
                    {
                        scene.HitTarget(true);
                        scene.BlowTarget(instances[i].X, instances[i].Y, true);
                    }
                    else
                    {
                        scene.HitTarget(false);
                        scene.BlowTarget(instances[i].X, instances[i].Y, false);
                    }

                    Respawn(i);
                }
            }

            return false;
        }
    }
}
