using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;

namespace Tank_Biathlon
{
    public class OptionsScene : Scene
    {
        private ContentManager content;
        private CheckBox cb_sound;
        private CheckBox cb_music;
        private Vector2 text_pos_sound;
        private Vector2 text_pos_music;

        public OptionsScene()
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

            Priority = 2;
            SceneManager.Priority = 2;

            Page = SceneManager.GetPage();

            Texture2D t_panel = SceneManager.Content.Load<Texture2D>("kennygui/grey_panel");

            Page.AddEntity(t_panel, GuiPage.Align.Top, 0f, 0f, 100f, 100f, 1.0f);

            Button b_back = Page.AddButton(GuiPage.Align.Bottom, 50f, 85f, "Back", 0);

            text_pos_sound = new Vector2(100f, 230f);
            text_pos_music = new Vector2(300f, 230f);

            cb_sound = Page.AddCheckBox(GuiPage.Align.Left, 30f, 50f, 0);
            cb_music = Page.AddCheckBox(GuiPage.Align.Right, 70f, 50f, 1);

            b_back.Event += OnBack;

            cb_sound.Event += OnCheckBox;
            cb_music.Event += OnCheckBox;

            cb_sound.Check(PlayerData.SoundOn);
            cb_music.Check(PlayerData.MusicOn);
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

            gs2d.SP.DrawString(Fonts.FontMenu, "Sound", text_pos_sound, Color.White);
            gs2d.SP.DrawString(Fonts.FontMenu, "Music", text_pos_music, Color.White);

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
            ExitScene();
        }

        public void OnBack(TouchLocationState state, byte id)
        {
            if (state == TouchLocationState.Released)
            {
                if (id == 0)
                {
                    SceneManager.Priority = 0;
                    ExitScene();
                }
            }
        }

        public void OnCheckBox(TouchLocationState state, byte id)
        {
            if (state == TouchLocationState.Released)
            {
                if (id == 0)
                    PlayerData.SoundOn = cb_sound.Pressed;
                else if (id == 1)
                    PlayerData.MusicOn = cb_music.Pressed;

                SoundManager.MusicOff = !PlayerData.MusicOn;
                SoundManager.SoundOff = !PlayerData.SoundOn;

                PlayerData.Save.SaveFile("player.dat");
            }
        }
    }
}
