using System;
using Microsoft.Xna.Framework;

namespace Iris
{
    public class Instance
    {
        private Vector2 pos;
        private byte type_id;

        public Instance()
        {
            pos = new Vector2(0f, 0f);
            type_id = 0;
        }

        public Instance(Vector2 pos, byte type_id)
        {
            this.pos = pos;
            this.type_id = type_id;
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void SetPos(Vector2 pos)
        {
            this.pos = pos;
        }

        public float GetX()
        {
            return pos.X;
        }

        public float GetY()
        {
            return pos.Y;
        }

        public void SetX(float x)
        {
            pos.X = x;
        }

        public void SetY(float y)
        {
            pos.Y = y;
        }

        public byte GetTypeId()
        {
            return type_id;
        }

        public void SetTypeId(byte id)
        {
            this.type_id = id;
        }
    }
}
