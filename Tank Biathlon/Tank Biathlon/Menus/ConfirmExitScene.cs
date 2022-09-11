using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class ConfirmExitScene : Scene
    {
        private ContentManager content;
        private Texture2D t_background;
        private Vector2 text_pos;

        public ConfirmExitScene()
        {
            content = null;
            TransitionTime = 0.2f;
            IsPopup = true;

            Priority = 10;
        }

        public override void Load()
        {
            //if (content == null)
            //    content = new ContentManager(SceneManager.Game.Services, "Content");

            content = SceneManager.Content;

            SceneManager.Priority = 10;

            Page = SceneManager.GetPage();

            Texture2D t_panel = SceneManager.Content.Load<Texture2D>("kennygui/grey_panel");

            Page.AddEntity(t_panel, GuiPage.Align.Top, 10f, 10f, 80f, 80f, 1.0f);

            text_pos = new Vector2(110f, 200f);

            Button b_yes = Page.AddButton(GuiPage.Align.Bottom, 50f, 45f, "Yes", 0);
            Button b_no = Page.AddButton(GuiPage.Align.Bottom, 50f, 65f, "No", 1);
            b_yes.Event += OnBack;
            b_no.Event += OnBack;
        }

        public override void Unload()
        {
            base.Unload();
            //content.Unload();
        }

        public override void Draw(Graphics2D gs2d)
        {
            base.Draw(gs2d);

            gs2d.Begin();
            gs2d.SP.DrawString(Fonts.FontMenu, "Want to exit?", text_pos, Color.White);
            gs2d.End();
        }

        public override void Update(float dt, bool has_focus, bool covered_by_other)
        {
            base.Update(dt, has_focus, covered_by_other);
        }

        public override void HandleInput(TouchCollection touches, float dt)
        {

        }

        public override void OnBackPressed()
        {
            ExitScene();
        }

        public void OnBack(TouchLocationState state, byte id)
        {
            if (state == TouchLocationState.Released)
            {
                if (id == 0)
                {
                    SceneManager.Priority = 0;
                    LoadingScene.Load(SceneManager, true, new MainBackgroundScene(), new MainMenuScene());
                }
                else if (id == 1)
                {
                    SceneManager.Priority = 0;
                    ExitScene();
                }
            }
        }
    }
}
