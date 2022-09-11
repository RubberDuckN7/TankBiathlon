using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Iris
{
    public class World
    {
        private List<Content> contents;
        private List<BaseType> types;
        private int screen_width;
        private int screen_height;
        private Vector2 offset;

        public World()
        {
            contents = new List<Content>();
            types = new List<BaseType>();
            screen_width = 800;
            screen_height = 480;
            offset = new Vector2(0f, 0f);
        }

        public void Draw(Graphics2D gs2d)
        {
            foreach (Content c in contents)
            {
                c.Draw(this, gs2d);
            }
        }

        public void Update(float dt)
        {
            foreach (Content c in contents)
            {
                c.Update(this, dt);
            }
        }

        public byte AddContent(Content content)
        {
            contents.Add(content);
            return (byte)(contents.Count - 1);
        }

        public Content GetContent(byte id)
        {
            return contents[id];
        }

        public void Clear()
        {
            foreach (Content c in contents)
            {
                c.Clear();
            }

            contents.Clear();
        }

        public void Clear(byte id)
        {
            if (id < 0 || id > contents.Count)
                return;
            contents[id].Clear();
        }

        public byte AddType(BaseType type)
        {
            types.Add(type);
            return (byte)(types.Count - 1);
        }

        public void AddInstance(byte content_id, Instance instance)
        {
            contents[content_id].AddInstance(instance);
        }

        public BaseType GetType(int id)
        {
            return types[id];
        }

        public int GetWidth()
        {
            return screen_width;
        }

        public int GetHeight()
        {
            return screen_height;
        }

        public void SetWidth(int width)
        {
            this.screen_width = width;
        }

        public void SetHeight(int height)
        {
            this.screen_height = height;
        }

        public Vector2 GetOffset()
        {
            return offset;
        }

        public float GetOffsetX()
        {
            return offset.X;
        }

        public float GetOffsetY()
        {
            return offset.Y;
        }

        public void SetOffset(Vector2 offset)
        {
            this.offset = offset;
        }

        public void SetOffsetX(float x)
        {
            offset.X = x;
        }

        public void SetOffsetY(float y)
        {
            offset.Y = y;
        }
    }
}
