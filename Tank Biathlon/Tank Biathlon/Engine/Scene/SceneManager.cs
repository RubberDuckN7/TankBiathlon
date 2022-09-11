using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class SceneManager : DrawableGameComponent
    {
        private List<Scene> scenes = new List<Scene>();
        private List<Scene> scenes_temp = new List<Scene>();

        private GuiManager gui_manager;
        private Graphics2D graphics2d;
        //private SpriteBatch sprite_batch;
        private float offset;
        private int priority = 0;
        private int width;
        private int height;
        private bool back_pressed;

        public SceneManager(Game game, int width, int height, bool landscape)
            : base(game)
        {
            if (landscape)
            {
                this.width = width;
                if ((width * 0.6f) < height)
                {
                    this.offset = (float)(height - width * 0.6f);
                    this.height = (int)(width * 0.6f);
                }
                else
                    this.height = height;
            }
            else
            {
                this.height = height;
                if ((height * 0.6f) < width)
                {
                    this.offset = (float)(width - height * 0.6f);
                    this.width = (int)(height * 0.6f);
                }
                else
                    this.width = width;
            }

            //this.height = height;
            //inv_width = 1.0f / (float)width;
            //inv_height = 1.0f / (float)height;

            back_pressed = false;
            gui_manager = new GuiManager(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load content belonging to the scene manager.
            ContentManager content = Game.Content;
            graphics2d = new Graphics2D(new SpriteBatch(GraphicsDevice));
            //sprite_batch = new SpriteBatch(GraphicsDevice);

            foreach (Scene scene in scenes)
            {
                scene.Load();
            }
        }

        protected override void UnloadContent()
        {
            foreach (Scene scene in scenes)
            {
                scene.Unload();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (scenes.Count > 0 && !back_pressed)
                {
                    back_pressed = true;
                    scenes[scenes.Count - 1].OnBackPressed();
                }
                //this.Game.Exit();
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Released)
                back_pressed = false;

            TouchCollection touches = TouchPanel.GetState();
            float dt = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            scenes_temp.Clear();

            foreach (Scene scene in scenes)
                scenes_temp.Add(scene);

            bool other_has_focus = false;
            bool covered_by_other = false;

            while (scenes_temp.Count > 0)
            {
                Scene scene = scenes_temp[scenes_temp.Count - 1];

                scenes_temp.RemoveAt(scenes_temp.Count - 1);

                // Should be named "has_focus"
                scene.Update(dt, !other_has_focus, covered_by_other);

                //if (scene.IsActive)
                //{
                    if (!other_has_focus)
                    {
                        other_has_focus = true;
                    }

                    if (scene.Priority >= priority)
                    {
                        scene.HandleInput(touches, dt);
                        if (scene.Page != null)
                            scene.Page.HandleInput(touches, dt);
                    }

                    if (!scene.IsPopup)
                        covered_by_other = true;
                //}
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Scene scene in scenes)
            {
                if (scene.SceneState == SceneState.Hidden)
                    continue;

                scene.Draw(graphics2d);
            }
        }

        public void Exit()
        {
            this.Game.Exit();
        }

        public void AddScene(Scene scene)
        {
            scene.SceneManager = this;
            scene.IsExiting = false;

            scene.Load();

            scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {
            scene.Unload();

            scenes.Remove(scene);
            scenes_temp.Remove(scene);
        }

        public GuiPage GetPage()
        {
            return new GuiPage(gui_manager);
        }

        public GuiManager Gui
        {
            get { return gui_manager; }
        }

        public Scene[] GetScenes()
        {
            return scenes.ToArray();
        }

        public Graphics2D Graphics2D
        {
            get { return graphics2d; }
        }

        public ContentManager Content
        {
            get { return Game.Content; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public float InvWidth
        {
            get { return 1.0f / (float)width; }
        }

        public float InvHeight
        {
            get { return 1.0f / (float)height; }
        }

        public float Offset
        {
            get { return offset; }
        }
    }
}
