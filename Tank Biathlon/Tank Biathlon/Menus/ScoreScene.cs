using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class ScoreScene : Scene
    {
        private ContentManager content;
        private Texture2D t_medal;
        private Vector2 text_pos;
        private Vector2 score_pos;
        private Rectangle bound_medal;
        private int score;
        private float speed;
        private float total_time;

        public ScoreScene(int score, float total_time, float speed)
        {
            this.score = score;
            this.total_time = total_time;
            this.speed = speed;

            content = null;
            TransitionTime = 0.2f;
            IsPopup = true;
        }

        public override void Load()
        {
            //if (content == null)
            //    content = new ContentManager(SceneManager.Game.Services, "Content");

            content = SceneManager.Content;

            if (speed < 200)
                t_medal = content.Load<Texture2D>("medals/medal_none");
            else if(speed > 200 && speed < 300)
                t_medal = content.Load<Texture2D>("medals/medal_bronze");
            else if(speed > 300 && speed < 400)
                t_medal = content.Load<Texture2D>("medals/medal_silver");
            else if(speed > 400 && speed < 500)
                t_medal = content.Load<Texture2D>("medals/medal_gold");
            else
                t_medal = content.Load<Texture2D>("medals/medal_platinum");


            SoundManager.StopMusic();
            SoundManager.PlayOnScore();

            SceneManager.Priority = 10;
            Priority = 10;

            Page = SceneManager.GetPage();

            Texture2D t_panel = SceneManager.Content.Load<Texture2D>("kennygui/panel");


            text_pos = new Vector2(110f, 200f);
            score_pos = new Vector2(text_pos.X, text_pos.Y + 70f);

            Page.AddEntity(t_panel, GuiPage.Align.Top, 10f, 20f, 80f, 70f, 1.0f);

            bound_medal = new Rectangle((int)(SceneManager.Width*0.5f - 50), (int)(SceneManager.Height*0.6f - 50),
                100, 100);

            Button b_back = Page.AddButton(GuiPage.Align.Bottom, 50f, 75f, "Back", 0);
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

            gs2d.SP.DrawString(Fonts.FontScore, "SCORE: ", text_pos, Color.White);
            Color color = Color.LightGreen;
            if (score == 0)
                color = Color.Red;
            gs2d.SP.DrawString(Fonts.FontScore, "" + score, score_pos, color);

            gs2d.Draw(t_medal, bound_medal);

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
            SceneManager.Priority = 0;
            LoadingScene.Load(SceneManager, true, new MainBackgroundScene(), new MainMenuScene());
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
            }
        }
    }
}
