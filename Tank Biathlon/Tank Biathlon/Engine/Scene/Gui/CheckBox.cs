using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class CheckBox : Element
    {
        public delegate void Callback(TouchLocationState state, byte id);
        private TypeCheckBox type;
        private Vector2 pos;
        private byte id;
        private bool pressed;
        private bool hide;
        private bool is_checked;

        public Callback Event;

        public CheckBox(TypeCheckBox type, float x, float y, byte id)
        {
            Rectangle b = new Rectangle((int)x, (int)y, (int)type.Width, (int)type.Height);

            this.type = type;
            this.pos = new Vector2(x, y);
            this.id = id;
            this.pressed = false;
            this.hide = false;
            this.Event = null;
            this.is_checked = false;
        }

        public override void Draw(Graphics2D gs2d)
        {
            Draw(gs2d, Color.White);
        }

        public override void Draw(Graphics2D gs2d, Color color)
        {
            Draw(gs2d, color, Vector2.Zero);
        }
        public override void Draw(Graphics2D gs2d, Color color, Vector2 offset)
        {
            if (hide)
                return;

            if (pressed)
                color = Color.DarkGray;

            Rectangle bounds = new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y),
                                               (int)type.Width, (int)type.Height);

            gs2d.Draw(type.TextureBackground, bounds, color, Vector2.Zero, 0.0f, type.LayerDepth);
            if (is_checked)
                gs2d.Draw(type.TextureCheck, bounds, color, Vector2.Zero, 0.0f, type.LayerDepth);
        }

        public override void HandleInput(TouchCollection touches, float dt)
        {
            if (hide)
                return;

            pressed = false;
            for (byte i = 0; i < touches.Count; i++)
            {
                Vector2 point = touches[i].Position;

                if (Collision.PointVsRectangle(point, pos, type.Bounds))
                {
                    if (touches[i].State == TouchLocationState.Pressed || touches[i].State == TouchLocationState.Moved)
                        pressed = true;
                    else if(touches[i].State == TouchLocationState.Released)
                        is_checked = !is_checked;

                    Event(touches[i].State, id);
                }
            }
        }

        public Vector2 Pos
        {
            get { return pos; }
        }

        public float X
        {
            get { return pos.X; }
        }

        public float Y
        {
            get { return pos.Y; }
        }

        public bool Pressed
        {
            get { return this.is_checked; }
        }

        public void Check(bool check)
        {
            this.is_checked = check;
        }
    }
}
