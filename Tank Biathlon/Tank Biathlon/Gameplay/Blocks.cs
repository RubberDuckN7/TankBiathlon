using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Iris;

namespace Tank_Biathlon
{
    public class Blocks
    {
        private class SingleBlock
        {
            public Vector2 pos;
            public byte health;

            public SingleBlock()
            {
                pos = new Vector2(0f, 0f);
                health = 0;
            }

            public SingleBlock(Vector2 pos, byte health)
            {
                this.pos = pos;
                this.health = health;
            }
        }

        private List<SingleBlock> instances;
        private Texture2D block_texture;
        
        private Vector2 screen_bounds;
        private Rectangle block_bounds;

        private GameScene scene;

        public Blocks(GameScene scene, Texture2D block_texture, int block_w, int block_h, Vector2 screen_bounds)
        {
            this.scene = scene;
            this.instances = new List<SingleBlock>();
            this.block_texture = block_texture;
            this.screen_bounds = screen_bounds;
            this.block_bounds = new Rectangle(0, 0, block_w, block_h);

            for (int i = 0; i < 5; i++)
            {
                instances.Add(new SingleBlock(new Vector2(), 2));
                Respawn(i);
            }
        }

        public void Draw(Graphics2D gs2d)
        {


            float shrink = (float)(block_bounds.Width * 0.15f);
            Rectangle b = new Rectangle(0, 0,
                (int)(block_bounds.Width - shrink*2), (int)(block_bounds.Height - shrink*2));



            foreach(SingleBlock i in instances)
            {
                if (i.pos.Y + block_bounds.Height > 0f)
                {
                    block_bounds.X = (int)i.pos.X;
                    block_bounds.Y = (int)i.pos.Y;

                    gs2d.SP.Draw(block_texture, block_bounds, Color.White);

                    //b.X = (int)(i.pos.X + shrink);
                    //b.Y = (int)(i.pos.Y + shrink);
                    //gs2d.SP.Draw(scene.debug_texture, b, Color.White);
                }
            }

            /*int count = (int)(screen_bounds.X / block_bounds.Width);
            Rectangle szb = new Rectangle(0, (int)(screen_bounds.Y-block_bounds.Width*4), 
                (int)block_bounds.Width, (int)block_bounds.Width);

            for (int i = 0; i < count; i++)
            {
                szb.X = (int)(i * block_bounds.Width);
                gs2d.SP.Draw(scene.debug_texture, szb, Color.White);
            }*/
        }

        public void Update(float step)
        {
            for(int i = 0; i < instances.Count; i++)
            {
                Vector2 v2 = instances[i].pos;
                v2.Y += step;
                instances[i].pos = v2;
                if(v2.Y > screen_bounds.Y)
                    Respawn(i);
            }
        }

        public void Respawn(int id)
        {
            //Vector2 v2 = instances[id];
            instances[id].pos = scene.Spawn();
            instances[id].health = 2;
        }

        public bool Collide(Rectangle tank, Bullet bullet)
        {
            if (!scene.IsAlive())
                return false;

            float shrink = (float)(block_bounds.Width * 0.15f);
            Rectangle b = new Rectangle(0, 0,
                (int)(block_bounds.Width - shrink * 2), (int)(block_bounds.Height - shrink * 2));

            for(int i = 0; i < instances.Count; i++)
            {
                b.X = (int)(instances[i].pos.X + shrink);
                b.Y = (int)(instances[i].pos.Y + shrink);

                if (Collision.RectangleVsRectangle(b, tank))
                {
                    scene.KillTank();
                    return true;
                }
                if (bullet.IsAlive() && Collision.PointVsRectangle(bullet.GetPos(), b))
                {
                    bullet.Kill();
                    scene.BulletHit(bullet.GetPos().X, bullet.GetPos().Y);
                    //if (instances[i].health > 0)
                    //    instances[i].health -= 1;
                    //else
                    //    Respawn(i);
                }
            }

            return false;
        }
    }
}
