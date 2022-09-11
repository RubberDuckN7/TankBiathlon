using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iris
{
    /// <summary>
    /// The loading screen coordinates transitions between the menu system and the
    /// game itself. Normally one screen will transition off at the same time as
    /// the next screen is transitioning on, but for larger transitions that can
    /// take a longer time to load their data, we want the menu system to be entirely
    /// gone before we start loading the game. This is done as follows:
    /// 
    /// - Tell all the existing screens to transition off.
    /// - Activate a loading screen, which will transition on at the same time.
    /// - The loading screen watches the state of the previous screens.
    /// - When it sees they have finished transitioning off, it activates the real
    ///   next screen, which may take a long time to load its data. The loading
    ///   screen will be the only thing displayed while this load is taking place.
    /// </summary>
    public class LoadingScene : Scene
    {
        bool loadingIsSlow;
        bool otherScenesAreGone;

        Scene[] scenes_to_load;

        private LoadingScene(SceneManager smanager, bool loadingIsSlow,
                              Scene[] scenes_to_load)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.scenes_to_load = scenes_to_load;

            TransitionTime = 0.5f;

            //TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Activates the loading screen.
        /// </summary>
        public static void Load(SceneManager smanager, bool loadingIsSlow,
                                params Scene[] scenes_to_load)
        {
            // Tell all the current screens to transition off.
            foreach (Scene scene in smanager.GetScenes())
                scene.ExitScene();

            // Create and activate the loading screen.
            LoadingScene loadingScreen = new LoadingScene(smanager,
                                                            loadingIsSlow,
                                                            scenes_to_load);

            smanager.AddScene(loadingScreen);
        }

        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(float dt, bool has_focus, bool covered_by_other)
        {
            base.Update(dt, has_focus, covered_by_other);

            // If all the previous screens have finished transitioning
            // off, it is time to actually perform the load.
            if (otherScenesAreGone)
            {
                SceneManager.RemoveScene(this);

                foreach (Scene scene in scenes_to_load)
                {
                    if (scene != null)
                    {
                        SceneManager.AddScene(scene);
                    }
                }

                GC.Collect();

                // Once the load has finished, we use ResetElapsedTime to tell
                // the  game timing mechanism that we have just finished a very
                // long frame, and that it should not try to catch up.
                SceneManager.Game.ResetElapsedTime();
            }
        }


        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(Graphics2D gs2d)
        {
            // If we are the only active screen, that means all the previous screens
            // must have finished transitioning off. We check for this in the Draw
            // method, rather than in Update, because it isn't enough just for the
            // screens to be gone: in order for the transition to look good we must
            // have actually drawn a frame without them before we perform the load.
            if ((SceneState == SceneState.Active) &&
                (SceneManager.GetScenes().Length == 1))
            {
                otherScenesAreGone = true;
            }

            // The gameplay screen takes a while to load, so we display a loading
            // message while that is going on, but the menus load very quickly, and
            // it would look silly if we flashed this up for just a fraction of a
            // second while returning from the game to the menus. This parameter
            // tells us how long the loading is going to take, so we know whether
            // to bother drawing the message.
            if (loadingIsSlow)
            {
                //SpriteBatch spriteBatch = SceneManager.SpriteBatch;
                //SpriteFont font = SceneManager.Font;

                //const string message = "Loading...";

                // Center the text in the viewport.
                //Viewport viewport = SceneManager.GraphicsDevice.Viewport;
                //Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                //Vector2 textSize = font.MeasureString(message);
                //Vector2 textPosition = (viewportSize - textSize) / 2;

                //Color color = Color.White * TimeAlpha;

                // Draw the text.
                gs2d.Begin();
                gs2d.Draw(SystemContent.LoadingTexture, SceneManager.GraphicsDevice.Viewport.Bounds, Color.White);
                gs2d.End();
                //sp.Begin();
                //sp.Draw(SystemContent.LoadingTexture, SceneManager.GraphicsDevice.Viewport.Bounds, Color.White);
                //sp.DrawString(font, message, textPosition, color);
                //sp.End();
            }
        }

    }
}
