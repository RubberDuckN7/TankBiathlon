using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Iris;
using System.Collections.Generic;

namespace Tank_Biathlon
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene()
        {
            IsPopup = true;
            TransitionTime = 0.5f;
            Priority = 0;
        }

        public override void Load()
        {
            SceneManager.Priority = 0;

            Page = SceneManager.GetPage();

            Texture2D t_panel = SceneManager.Content.Load<Texture2D>("kennygui/panel");

            Page.AddEntity(t_panel, GuiPage.Align.Top, 10f, 20f, 80f, 52f, 1.0f);

            Button bplay = Page.AddButton(GuiPage.Align.Top, 50.0f, 30.0f, "Play", (byte)0);
            Button boptions = Page.AddButton(GuiPage.Align.Top, 50.0f, 45.0f, "Options", (byte)1);
            Button bcredits = Page.AddButton(GuiPage.Align.Top, 50.0f, 60.0f, "Credits", (byte)2);
            //Button bcredits = Page.AddButton(GuiPage.Align.Top, 50.0f, 60.0f, "Credits", (byte)3);

            bplay.Event += OnEvent;
            boptions.Event += OnEvent;
            bcredits.Event += OnEvent;

            if (PlayerData.Save.FileExist("player.dat"))
                PlayerData.Save.LoadFile("player.dat");

            SoundManager.SoundOff = !PlayerData.SoundOn;
            SoundManager.MusicOff = !PlayerData.MusicOn;
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Draw(Graphics2D gs2d)
        {
            gs2d.Begin();

 
            gs2d.End();

            base.Draw(gs2d);
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
            SceneManager.Exit();
        }

        public void OnEvent(TouchLocationState state, byte id)
        {
            if (state == TouchLocationState.Released)
            {
                if (id == 0)
                    LoadingScene.Load(SceneManager, true, new GameScene());
                else if (id == 1)
                    SceneManager.AddScene(new OptionsScene());
                else if (id == 2)
                    SceneManager.AddScene(new CreditsScene());
            }
        }
    }
}
