using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class GuiPage
    {
        public enum Align
        {
            Top,
            Bottom,
            Left,
            Right,
        }

        private GuiManager manager;
        private List<Element> elements_top;
        private List<Element> elements_bottom;
        private List<Element> elements_right;
        private List<Element> elements_left;

        public GuiPage(GuiManager manager)
        {
            this.manager = manager;

            elements_top = new List<Element>();
            elements_bottom = new List<Element>();
            elements_right = new List<Element>();
            elements_left = new List<Element>();
        }

        public void Draw(Graphics2D gs2d, float time_alpha)
        {
            float width = (float)(manager.Manager.Width);
            float height = (float)(manager.Manager.Height);

            gs2d.Begin();

            time_alpha = 1.0f - time_alpha;

            Vector2 offset = new Vector2();

            for (byte i = 0; i < elements_top.Count; i++)
            {
                offset.X = 0.0f;
                offset.Y = -height * time_alpha;
                offset.Y = MathHelper.Clamp(offset.Y, -480f, 0f);

                elements_top[i].Draw(gs2d, Color.White, offset);
            }

            for (byte i = 0; i < elements_bottom.Count; i++)
            {
                offset.X = 0.0f;
                offset.Y = height * time_alpha;
                offset.Y = MathHelper.Clamp(offset.Y, 0f, 480f);

                elements_bottom[i].Draw(gs2d, Color.White, offset);
            }

            for (byte i = 0; i < elements_left.Count; i++)
            {
                offset.X = -width * time_alpha;
                offset.Y = 0f;
                offset.X = MathHelper.Clamp(offset.X, -800f, 0f);

                elements_left[i].Draw(gs2d, Color.White, offset);
            }

            for (byte i = 0; i < elements_right.Count; i++)
            {
                offset.X = width * time_alpha;
                offset.Y = 0f;
                offset.X = MathHelper.Clamp(offset.X, 0f, 800f);

                elements_right[i].Draw(gs2d, Color.White, offset);
            }

            gs2d.End();
        }

        public void HandleInput(TouchCollection touches, float dt)
        {
            for (byte i = 0; i < elements_top.Count; i++)
            {
                elements_top[i].HandleInput(touches, dt);
            }

            for (byte i = 0; i < elements_bottom.Count; i++)
            {
                elements_bottom[i].HandleInput(touches, dt);
            }

            for (byte i = 0; i < elements_left.Count; i++)
            {
                elements_left[i].HandleInput(touches, dt);
            }

            for (byte i = 0; i < elements_right.Count; i++)
            {
                elements_right[i].HandleInput(touches, dt);
            }
        }

        public Entity AddEntity(Texture2D texture, Align align, float x, float y, float w, float h, float layer_depth)
        {
            Entity e = null;

            x = x / 100.0f;
            y = y / 100.0f;
            w = w / 100.0f;
            h = h / 100.0f;

            x = x * (float)(manager.Manager.Width);
            y = y * (float)(manager.Manager.Height);
            w = w * (float)(manager.Manager.Width);
            h = h * (float)(manager.Manager.Height);

            if (layer_depth < 0.0f)
                layer_depth = manager.LayerDepth;

            e = new Entity(texture, new Rectangle((int)x, (int)y, (int)w, (int)h), layer_depth);

            AddElement(e, align);
            return e;
        }

        public Button AddButton(Align align, float x, float y, String name, byte id)
        {
            Button b = null;
            if (manager.GetTypeButton == null)
                return null;

            x = x / 100.0f;
            y = y / 100.0f;

            x = x * (float)(manager.Manager.Width) - (float)manager.GetTypeButton.Width*0.5f;
            y = y * (float)(manager.Manager.Height) - (float)manager.GetTypeButton.Height * 0.5f;

            b = new Button(manager.GetTypeButton, x, y, name, id);

            AddElement(b, align);
            return b;
        }

        public Button AddButton(TypeButton type, Align align, float x, float y, String name, byte id)
        {
            Button b = null;

            x = x / 100.0f;
            y = y / 100.0f;

            x = x * (float)(manager.Manager.Width) - (float)manager.GetTypeButton.Width * 0.5f;
            y = y * (float)(manager.Manager.Height) - (float)manager.GetTypeButton.Height * 0.5f;

            b = new Button(type, x, y, name, id);

            AddElement(b, align);
            return b;
        }

        public CheckBox AddCheckBox(Align align, float x, float y, byte id)
        {
            CheckBox b = null;
            if (manager.GetTypeButton == null)
                return null;

            x = x / 100.0f;
            y = y / 100.0f;

            x = x * (float)(manager.Manager.Width) - (float)manager.GetTypeCheckBox.Width * 0.5f;
            y = y * (float)(manager.Manager.Height) - (float)manager.GetTypeCheckBox.Height * 0.5f;

            b = new CheckBox(manager.GetTypeCheckBox, x, y, id);

            AddElement(b, align);
            return b;
        }

        public void AddElement(Element e, Align align)
        {
            switch (align)
            {
                case Align.Top:
                    elements_top.Add(e);
                    break;
                case Align.Bottom:
                    elements_bottom.Add(e);
                    break;
                case Align.Left:
                    elements_left.Add(e);
                    break;
                case Align.Right:
                    elements_right.Add(e);
                    break;
                default:
                    elements_top.Add(e);
                    break;
            }
        }

        public GuiManager GetManager()
        {
            return manager; 
        }
    }
}
