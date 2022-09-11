using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class TutorialScene : Scene
    {
        private ContentManager content;
        private Texture2D t_background;

        public TutorialScene()
        {
            content = null;
        }

        public override void Load()
        {
            if (content == null)
                content = new ContentManager(SceneManager.Game.Services, "Content");

            t_background = content.Load<Texture2D>("background");
        }

        public override void Unload()
        {
            base.Unload();
            content.Unload();
        }

        public override void Draw(Graphics2D gs2d)
        {
            gs2d.Begin();

            gs2d.Draw(t_background, SceneManager.GraphicsDevice.Viewport.Bounds, Color.White, 0.2f);

            gs2d.End();
        }

        public override void Update(float dt, bool has_focus, bool covered_by_other)
        {
            base.Update(dt, has_focus, covered_by_other);
        }

        public override void HandleInput(TouchCollection touches, float dt)
        {

        }
    }
}
