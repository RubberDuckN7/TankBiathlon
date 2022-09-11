using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public enum SceneState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class Scene
    {
        private SceneManager scene_manager = null;
        private SceneState scene_state = SceneState.TransitionOn;
        private GuiPage page_gui = null;
        private int priority = 0;
        private float time_alpha = 0.0f;
        private float transition_time;
        private bool has_focus = false;
        private bool is_exiting = false;
        private bool is_popup = false;

        public virtual void Load() { }

        public virtual void Unload() { }

        public virtual void Update(float dt, bool has_focus, bool covered_by_other)
        {
            this.has_focus = has_focus;

            if (is_exiting)
            {
                // If the scene is going away to die, it should transition off.
                scene_state = SceneState.TransitionOff;

                if (!UpdateTransition(dt, transition_time, -1))
                {
                    // When the transition finishes, remove the scene.
                    SceneManager.RemoveScene(this);
                }
            }
            else if (covered_by_other)
            {
                // If the scene is covered by another, it should transition off.
                if (UpdateTransition(dt, transition_time, 1))
                {
                    // Still busy transitioning.
                    scene_state = SceneState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    scene_state = SceneState.Hidden;
                }
            }
            else
            {
                // Otherwise the scene should transition on and become active.
                if (UpdateTransition(dt, transition_time, 1))
                {
                    // Still busy transitioning.
                    scene_state = SceneState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    scene_state = SceneState.Active;
                }
            }
        }

        public virtual void HandleInput(TouchCollection touches, float dt) { }

        public virtual void Draw(Graphics2D gs2d)
        {
            if (page_gui != null)
                page_gui.Draw(gs2d, time_alpha);
        }

        public virtual void OnBackPressed()
        { }

        public void ExitScene()
        {
            if (transition_time < 0.1f)
            {
                // If the scene has a zero transition time, remove it immediately.
                SceneManager.RemoveScene(this);
            }
            else
            {
                // Otherwise flag that it should transition off and then exit.
                is_exiting = true;
            }
        }

        private bool UpdateTransition(float dt, float time_to_transit, int direction)
        {
            dt = dt / time_to_transit;

            // How much should we move by?
            time_alpha += dt * direction;

            // Did we reach the end of the transition?
            if (((direction < 0) && (time_alpha <= 0)) ||
                ((direction > 0) && (time_alpha >= 1)))
            {
                time_alpha = MathHelper.Clamp(time_alpha, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }

        public bool IsPopup
        {
            get { return is_popup; }
            protected set { is_popup = value; }
        }

        public float TimeAlpha
        {
            get { return time_alpha; }
        }

        public float TransitionTime
        {
            get { return transition_time; }
            set { transition_time = value; }
        }

        public SceneState SceneState
        {
            get { return scene_state; }
            protected set { scene_state = value; }
        }

        public bool IsExiting
        {
            get { return is_exiting; }
            protected internal set { is_exiting = value; }
        }

        public bool IsActive
        {
            get
            {
                return has_focus &&
                       (scene_state == SceneState.TransitionOn ||
                        scene_state == SceneState.Active);
            }
        }

        public SceneManager SceneManager
        {
            get { return scene_manager; }
            internal set { scene_manager = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public GuiPage Page
        {
            get { return page_gui; }
            set { page_gui = value; }
        }
    } // SceneInterface
}
