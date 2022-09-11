using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class MainBackgroundScene : Scene
    {
        private ContentManager content;
        private Texture2D [] backgrounds;
        private Rectangle bounds;
        private float time;
        private bool left;

        public MainBackgroundScene()
        {
            content = null;
            time = 0f;
            left = true;
        }

        public override void Load()
        {
            if (content == null)
                content = new ContentManager(SceneManager.Game.Services, "Content");
            backgrounds = new Texture2D[3];

            backgrounds[0] = content.Load<Texture2D>("backgrounds/background_pix");
            backgrounds[1] = content.Load<Texture2D>("backgrounds/background_pix");
            backgrounds[2] = content.Load<Texture2D>("backgrounds/background_pix");

            int w = (int)(960);
            int h = (int)(800);
            bounds = new Rectangle(0, 0, w, h);
        }

        public override void Unload()
        {
            base.Unload();
            content.Unload();
        }

        public override void Draw(Graphics2D gs2d)
        {
            gs2d.Begin();

            gs2d.Draw(backgrounds[0], bounds, Color.White, 0.2f);

            gs2d.End();
        }

        public override void Update(float dt, bool has_focus, bool covered_by_other)
        {
            base.Update(dt, has_focus, covered_by_other);
            if (left)
            {
                time -= dt;
                bounds.X -= (int)(dt * 40f);

                if (bounds.X + bounds.Width < SceneManager.Width)
                {
                    bounds.X = -bounds.Width/2;
                    left = false;
                }
            }
            else
            {
                bounds.X += (int)(dt * 40f);
                if (bounds.X > 0)
                {
                    bounds.X = 0;
                    left = true;
                }
            }
        }

        public override void HandleInput(TouchCollection touches, float dt)
        {

        }
    }
}
