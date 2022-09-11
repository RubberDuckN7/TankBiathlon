using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class CreditsScene : Scene
    {
        private ContentManager content;
        private Vector2 text_pos;

        public CreditsScene()
        {
            content = null;
            TransitionTime = 0.2f;
            IsPopup = true;
        }

        public override void Load()
        {
            //if (content == null)
            //    content = new ContentManager(SceneManager.Game.Services, "Content");

            content = SceneManager.Content;

            Page = SceneManager.GetPage();

            Texture2D t_panel = SceneManager.Content.Load<Texture2D>("kennygui/grey_panel");

            text_pos = new Vector2(50f, 80f);

            Page.AddEntity(t_panel, GuiPage.Align.Top, 0f, 0f, 100f, 100f, 1.0f);

            Button b_back = Page.AddButton(GuiPage.Align.Bottom, 50f, 85f, "Back", 0);
            b_back.Event += OnBack;
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

            String credits = "";

            credits += "Coding: \nOleg Kustov\n";
            credits += "\nGraphics: \n";
            credits += "opengameart.org\n";
            credits += "Buch\n";
            credits += "yughues\n";
            credits += "Sullivan\n";
            credits += "Oleg\n\n";

            credits += "Sound and music\n";
            credits += "http://www.freesfx.co.uk";


            gs2d.SP.DrawString(Fonts.FontMenu, credits, text_pos, Color.LightGreen);

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
                    ExitScene();
                }
            }
        }
    }
}
