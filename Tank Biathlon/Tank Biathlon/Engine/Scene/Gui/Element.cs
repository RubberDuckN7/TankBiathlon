using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class Element
    {
        public virtual void Draw(Graphics2D gs2d) { }
        public virtual void Draw(Graphics2D gs2d, Color color) { }
        public virtual void Draw(Graphics2D gs2d, Color color, Vector2 offset) { }
        public virtual void HandleInput(TouchCollection touches, float dt) { }
    }
}
