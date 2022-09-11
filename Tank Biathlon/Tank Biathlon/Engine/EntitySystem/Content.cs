using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris
{
    public class Content
    {
        private List<Instance> instances;
        private byte offset;

        private bool IsInsideScreen(World world, BaseType type, Instance instance)
        {
            if (world.GetOffsetX() > instance.GetX() + type.GetWidth())
                return false;
            if (world.GetOffsetY() > instance.GetY() + type.GetHeight())
                return false;
            if (world.GetOffsetX() + world.GetWidth() < instance.GetX())
                return false;
            if (world.GetOffsetY() + world.GetHeight() < instance.GetY())
                return false;

            return true;
        }

        public Content()
        {
            instances = new List<Instance>();
            offset = 0;
        }

        public void Draw(World world, Graphics2D gs2d)
        {
            foreach (Instance i in instances)
            {
                BaseType type = world.GetType(i.GetTypeId());
                
                if(IsInsideScreen(world, type, i))
                    type.Draw(world, this, gs2d, i);
            }
        }

        public void Update(World world, float dt)
        {
            int count = 0;
            while (count < instances.Count)
            {
                BaseType type = world.GetType(instances[count].GetTypeId());
                type.Update(world, this, instances[count], dt);

                count++;
                count -= (int)offset;
                count = (count < 0) ? 0 : count;
                offset = 0;
            }
        }

        public void RemoveInstance(Instance instance)
        {
            instances.Remove(instance);
            offset += 1;
        }

        public void RemoveInstance(int id)
        {
            instances.RemoveAt(id);
            offset += 1;
        }

        public byte AddInstance(Instance instance)
        {
            instances.Add(instance);
            return (byte)(instances.Count - 1);
        }

        public void Clear()
        {
            instances.Clear();
            offset = 0;
        }
    }
}
