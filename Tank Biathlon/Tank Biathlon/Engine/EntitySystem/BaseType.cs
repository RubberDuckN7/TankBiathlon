using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Iris
{
    public class BaseType
    {
        protected Rectangle bounds;
        protected Texture2D texture;

        public virtual void Draw(World world, Content content, Graphics2D gs2d, Instance instance)
        {
            bounds.X = (int)(instance.GetX() - world.GetOffsetX());
            bounds.Y = (int)(instance.GetY() - world.GetOffsetY());
            gs2d.Draw(texture, bounds);
        }

        public virtual void Update(World world, Content content, Instance instance, float dt)
        {
            BaseType type = world.GetType(instance.GetTypeId());
            if (instance.GetY() > world.GetOffsetY() + world.GetHeight())
            {
                instance.SetY(instance.GetY() - world.GetHeight());
            }
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }

        public void SetBounds(Rectangle bounds)
        {
            this.bounds = bounds;
        }

        public int GetWidth()
        {
            return bounds.Width;
        }

        public int GetHeight()
        {
            return bounds.Height;
        }

        public void SetWidth(int width)
        {
            bounds.Width = width;
        }

        public void SetHeight(int height)
        {
            bounds.Height = height;
        }
    }
}
