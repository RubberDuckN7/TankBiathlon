using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class GameScene : Scene
    {
        private ContentManager content;
        private SpriteFont font;

        private SpawnArea spawn;
        private Blocks blocks;
        private Targets targets;

        private Animation a_explosion;
        private Animation a_hit;
        private Animation a_blow;
        private Animation a_tank;

        private Actor tank;

        private Texture2D t_tile;
        private Texture2D t_cross;
        private Texture2D t_check;
        private Rectangle bounds_tile;
        private Rectangle bounds_cross;
        private Vector2[] v2_tiles;

        private Entity e_left;
        private Entity e_right;
        private Entity e_fire;

        private Bullet bullet;
        private Vector2 tank_dir;

        private bool b_fire;
        private bool b_left;
        private bool b_right;
        private bool tank_alive;
        private bool bfinished;

        private float rotation;
        private float speed;
        private float saved_speed;

        private float last_y;

        private float total_time;

        
        private int score;

        private bool bullseye;


        private String debug_msg = "";
        private String debug_pos = "";
        public Texture2D debug_texture;



        public GameScene()
        {
            content = null;

            tank = new Actor();
            tank_dir = new Vector2(0f, 1f);
            
            b_fire = false;
            b_left = false;
            b_right = false;
            tank_alive = true;
            bfinished = false;

            rotation = 0.0f;
            speed = 150f;
            total_time = 0f;
            score = 0;
        }

        public override void Load()
        {
            if (content == null)
                content = new ContentManager(SceneManager.Game.Services, "Content");

            font = content.Load<SpriteFont>("DefaultFont");

            Texture2D t_fire = content.Load<Texture2D>("trialgui/fire");
            Texture2D t_left = content.Load<Texture2D>("trialgui/left");
            Texture2D t_right = content.Load<Texture2D>("trialgui/right");

            Texture2D t_tank = content.Load<Texture2D>("tank");
            Texture2D t_bullet = content.Load<Texture2D>("bullet");
            Texture2D t_block = content.Load<Texture2D>("enemy");
            Texture2D t_target = content.Load<Texture2D>("target");
                      t_tile = content.Load<Texture2D>("tile");
                      t_cross = content.Load<Texture2D>("cross");
                      t_check = content.Load<Texture2D>("check");

            Texture2D [] t_expl = new Texture2D[11];
            for (int i = 0; i < t_expl.Length; i++)
            {
                t_expl[i] = content.Load<Texture2D>("explosion/explosion_a" + (i + 1));
            }

            Texture2D[] t_hit = new Texture2D[7];
            for (int i = 0; i < t_hit.Length; i++)
            {
                t_hit[i] = content.Load<Texture2D>("hit/bhit" + (i + 1));
            }

            Texture2D[] t_targexpl = new Texture2D[5];
            for (int i = 0; i < t_targexpl.Length; i++)
            {
                t_targexpl[i] = content.Load<Texture2D>("explode_target/texpl" + (i + 1));
            }

            Texture2D[] t_tank_a = new Texture2D[2];
            for (int i = 0; i < t_tank_a.Length; i++)
            {
                t_tank_a[i] = content.Load<Texture2D>("tank_anim/tank_a" + (i + 1));
            }

            int tank_size = (int)(SceneManager.Width * 0.18f);

            a_explosion = new Animation(t_expl, 80, 80, 94.3f, false);
            a_hit = new Animation(t_hit, 50, 50, 94.3f, false);
            a_blow = new Animation(t_targexpl, tank_size, tank_size, 100f, false);
            a_tank = new Animation(t_tank_a, 0, 0, 20f, true);

            bounds_tile = new Rectangle(0, 0, SceneManager.Width, SceneManager.Width);
            v2_tiles = new Vector2[3];
            float tile_offset = (float)(SceneManager.Height - bounds_tile.Height);
            for (int i = 0; i < v2_tiles.Length; i++)
            {
                v2_tiles[i] = new Vector2(0f, tile_offset);
                tile_offset -= (float)bounds_tile.Height;
            }

            last_y = tile_offset + (float)bounds_tile.Height;


            debug_texture = content.Load<Texture2D>("bounds");

            float size = (float)(SceneManager.Width/3.5);

            e_fire = new Entity(t_fire, new Rectangle((int)(size), SceneManager.Height-(int)(size), 
                (int)(SceneManager.Width-size*2), (int)(size)), 1.0f);

            e_left = new Entity(t_left, new Rectangle(0, (int)(SceneManager.Height - size), (int)size, (int)size), 1.0f);
            e_right = new Entity(t_right, new Rectangle((int)(SceneManager.Width - size), (int)(SceneManager.Height - size), 
                (int)size, (int)size), 1.0f);

            
            //e_tank = new Entity(t_tank, , 1.0f);

            tank.texture = t_tank;
            tank.bounds = new Rectangle((int)(SceneManager.Width * 0.5 - tank_size * 0.5),
                                                      (int)(SceneManager.Height * 0.75 - tank_size * 0.5),
                                                      tank_size, tank_size);
            tank.pos.X = (float)(tank.bounds.X);
            tank.pos.Y = (float)(tank.bounds.Y);

            int bullet_size = (int)(SceneManager.Width * 0.05f);
            Entity e_bullet = new Entity(t_bullet, new Rectangle(0, 0, bullet_size, bullet_size), 1.0f);
            bullet = new Bullet(e_bullet, 600f, 1.1f);

            int cross_size = (int)(tank_size * 0.5);
            bounds_cross = new Rectangle(-200, -200, cross_size, cross_size);
            


            
            //spawn = new SpawnArea((int)(SceneManager.Width / tank_size), (int)(SceneManager.Height/ tank_size), (float)tank_size);
            spawn = new SpawnArea((float)SceneManager.Width, 5, 20, tank_size);
            
            blocks = new Blocks(this, t_block, (int)(tank_size * 0.8f), (int)(tank_size*0.8f),
                new Vector2((float)SceneManager.Width, (float)SceneManager.Height));

            targets = new Targets(this, t_target, tank_size, (int)(tank_size*0.5f),
                new Vector2((float)SceneManager.Width, (float)SceneManager.Height));

            SoundManager.StartMusic();
        }

        public override void Unload()
        {
            base.Unload();
            SoundManager.StopAll();
            content.Unload();
        }

        public override void Draw(Graphics2D gs2d)
        {
            gs2d.Begin();

            for (int i = 0; i < v2_tiles.Length; i++)
            {
                bounds_tile.X = (int)(v2_tiles[i].X);
                bounds_tile.Y = (int)(v2_tiles[i].Y);

                gs2d.SP.Draw(t_tile, bounds_tile, Color.White);
            }

            blocks.Draw(gs2d);
            targets.Draw(gs2d);

            if(b_fire)
                e_fire.Draw(gs2d, Color.Gray);
            else
                e_fire.Draw(gs2d);

            if(b_left)
                e_left.Draw(gs2d);
            else
                e_left.Draw(gs2d, Color.Gray);

            if(b_right)
                e_right.Draw(gs2d);
            else
                e_right.Draw(gs2d, Color.Gray);



            

            tank.bounds.X = (int)(tank.bounds.Width / 2 + tank.pos.X);
            tank.bounds.Y = (int)(tank.bounds.Height / 2 + tank.pos.Y);

            bullet.Draw(gs2d);
            gs2d.SP.Draw(a_tank.GetTexture(), tank.bounds, null, Color.White, MathHelper.ToRadians(rotation),
                new Vector2(tank.texture.Width / 2, tank.texture.Height / 2), SpriteEffects.None, 1f);

            //gs2d.SP.Draw(tank.texture, tank.bounds, null, Color.White, MathHelper.ToRadians(rotation),
            //    new Vector2(tank.texture.Width / 2, tank.texture.Height / 2), SpriteEffects.None, 1f);

            gs2d.SP.Draw(t_cross, bounds_cross, Color.White);

            //tank.bounds.X = (int)tank.pos.X;
            //tank.bounds.Y = (int)tank.pos.Y;
            //gs2d.SP.Draw(debug_texture, tank.bounds, Color.White);

            
            a_explosion.Draw(gs2d);
            a_hit.Draw(gs2d);
            a_blow.Draw(gs2d);

            if (a_blow.IsAlive() && bullseye)
            {
                gs2d.SP.Draw(t_check, a_blow.GetBounds(), Color.White);
            }

            debug_msg = "Time: " + total_time + " Speed: " + speed;

            //gs2d.SP.DrawString(font, debug_msg, Vector2.Zero, Color.Red);
            //gs2d.SP.DrawString(font, debug_pos, new Vector2(0f, 20f), Color.Red);

            gs2d.End();
        }

        public override void Update(float dt, bool has_focus, bool covered_by_other)
        {
            base.Update(dt, has_focus, covered_by_other);
            if (!has_focus)
                return;


            float total_speed = 0f;
            if (tank_alive)
            {
                speed += 1.633f * dt;
                total_time += dt;
                total_speed = speed * MathHelper.Clamp(tank_dir.Y, 0.75f, 1.0f);
            }
            



            blocks.Update(total_speed * dt);
            targets.Update(total_speed * dt);
            spawn.Move(total_speed * dt);

            last_y += total_speed * dt;

            for (int i = 0; i < v2_tiles.Length; i++)
            {
                v2_tiles[i].Y += total_speed * dt;

                if (v2_tiles[i].Y > SceneManager.Height)
                {
                    v2_tiles[i].Y = (float)(last_y - bounds_tile.Height);
                    last_y -= (float)bounds_tile.Height;
                }
            }



            if (b_right)
            {
                rotation += 150f * dt;
                rotation = MathHelper.Clamp(rotation, -70f, 70f);
                tank_dir = Tools.DirFromAngle2D(rotation);
                //debug_msg = "X: " + tank_dir.X + " Y: " + tank_dir.Y;
            }
            else if (b_left)
            {
                rotation -= 150f * dt;
                rotation = MathHelper.Clamp(rotation, -70f, 70f);
                tank_dir = Tools.DirFromAngle2D(rotation);
                //debug_msg = "X: " + tank_dir.X + " Y: " + tank_dir.Y;
            }

            bounds_cross.X = (int)(tank.pos.X + (tank.bounds.Width - bounds_cross.Width) * 0.5f + tank_dir.X * 140f);
            bounds_cross.Y = (int)(tank.pos.Y - tank_dir.Y * 140f);
            
            if(tank_alive)
                tank.pos.X += (float)(tank_dir.X * 220f * dt);

            if (tank_alive && (tank.pos.X < -(tank.bounds.Width*0.15f) ||
                tank.pos.X + tank.bounds.Width > SceneManager.Width + (tank.bounds.Width * 0.15f)))
            {
                KillTank();
            }

            debug_pos = "Score: " + score;

            bullet.Update(dt);
            a_hit.Update(dt);
            a_blow.Update(dt);
            a_tank.Update(dt);
            
            
            tank.bounds.X = (int)(tank.pos.X);
            tank.bounds.Y = (int)(tank.pos.Y);
            blocks.Collide(tank.bounds, bullet);
            targets.Collide(tank.bounds, bullet);

            if (a_explosion.Update(dt))
            {
                if (tank_alive == false && !bfinished)
                {
                    bfinished = true;
                    SceneManager.AddScene(new ScoreScene(score, total_time, saved_speed));
                }
            }
        }

        public override void OnBackPressed()
        {
            SceneManager.AddScene(new ConfirmExitScene());
        }

        public override void HandleInput(TouchCollection touches, float dt)
        {
            for (int i = 0; i < touches.Count; i++)
            {
                if (touches[i].State == TouchLocationState.Pressed || touches[i].State == TouchLocationState.Moved)
                {
                    Vector2 point = touches[i].Position;

                    if (Collision.PointVsRectangle(point, e_fire.Bounds))
                    {
                        b_fire = true;

                        Vector2 dir = Tools.DirFromAngle2D(rotation);
                        Vector2 pos = new Vector2((float)(tank.pos.X + (tank.bounds.Width - bullet.GetBounds().Width) * 0.5f),
                                                  (float)(tank.pos.Y + (tank.bounds.Height - bullet.GetBounds().Height) * 0.5f));

                        dir.Y = -dir.Y;

                        //debug_msg = "X: " + dir.X + " Y: " + dir.Y;
                        bullet.Fire(pos, dir);
                    }
                    else
                    {
                        b_fire = false;
                    }
                    if (Collision.PointVsRectangle(point, e_left.Bounds))
                    {
                        b_left = true;
                    }
                    else
                    {
                        b_left = false;
                    }
                    if (Collision.PointVsRectangle(point, e_right.Bounds))
                    {
                        b_right = true;
                    }
                    else
                    {
                        b_right = false;
                    }
                }
                else if (touches[i].State == TouchLocationState.Released)
                {
                    b_fire = false;
                    b_left = false;
                    b_right = false;
                }
            }
        }

        public void KillTank()
        {
            a_explosion.Play(tank.pos.X, tank.pos.Y);
            tank_alive = false;
            saved_speed = speed;
            speed = 0.0f;
        }

        public void CrashTarget(float x, float y)
        {

        }

        public void HitTarget(bool bullseye)
        {
            score += (int)(2*(speed/100f));
            this.bullseye = bullseye;
            if (bullseye)
            {
                score += (int)(4f * (speed/100f));
            }
            else
            {
            }

            SoundManager.PlayOnBlowup();
        }

        public Vector2 Spawn()
        {
            return spawn.Spawn();
        }

        public bool IsAlive()
        {
            return tank_alive;
        }

        public void BulletHit(float x, float y)
        {
            a_hit.Play(x, y);
        }

        public void BlowTarget(float x, float y, bool bullseye)
        {
            a_blow.Play(x, y);
        }
    }
}
