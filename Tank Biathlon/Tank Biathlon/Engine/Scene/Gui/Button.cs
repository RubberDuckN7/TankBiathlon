using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class Button : Element
    {
        public delegate void Callback(TouchLocationState state, byte id);
        private TypeButton type;
        private Vector2 pos;
        private Vector2 text_pos;
        private String name;
        private byte id;
        private bool pressed;
        private bool hide;

        public Callback Event;

        public Button(TypeButton type, float x, float y, String name, byte id)
        {
            Rectangle b = new Rectangle((int)x, (int)y, (int)type.Width, (int)type.Height);

            this.type = type;
            this.pos = new Vector2(x, y);
            this.text_pos = Tools.GetTextPosition(type.Font, b, name);
            this.name = name;
            this.id = id;
            this.pressed = false;
            this.hide = false;
            this.Event = null;
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

            gs2d.Draw(type.Texture, bounds, color, Vector2.Zero, 0.0f, type.LayerDepth);
 
            text_pos += offset;
            gs2d.SP.DrawString(type.Font, name, text_pos, color);
            text_pos -= offset;
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
                    Event(touches[i].State, id);

                    if (touches[i].State == TouchLocationState.Pressed || touches[i].State == TouchLocationState.Moved)
                        pressed = true;
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
    }
}
