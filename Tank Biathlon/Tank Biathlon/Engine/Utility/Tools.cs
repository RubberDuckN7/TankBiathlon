using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iris
{
    public static class Tools
    {
        private static readonly Random random = new Random();

        public static float Random(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        public static float Random()
        {
            return (float)random.NextDouble();
        }

        public static Vector2 GetTextPosition(SpriteFont font, Rectangle bounds, string name)
        {
            Vector2 text = Vector2.Zero;

            text = font.MeasureString(name);

            float sx = (float)(bounds.Width) - text.X;
            float sy = (float)(bounds.Height) - text.Y;

            text.X = (float)(bounds.X) + sx * 0.5f;
            text.Y = (float)(bounds.Y) + sy * 0.5f;

            return text;
        }

        public static float Angle(Vector3 v1, Vector3 v2)
        {
            float dot = Vector3.Dot(v1, v2);
            dot = dot / (v1.Length() * v2.Length());

            double acos = Math.Acos((double)dot);

            return (float)(acos * 180.0f / Math.PI);
        }

        public static float AngleR(Vector3 v1, Vector3 v2)
        {
            float dot = Vector3.Dot(v1, v2);
            dot = dot / (v1.Length() * v2.Length());

            double acos = Math.Acos((double)dot);

            return (float)acos;
        }

        public static Vector3 DirFromAngle3D(float angle_x, float angle_y)
        {
            Vector3 dir = new Vector3(
                           (float)(Math.Sin(MathHelper.ToRadians(angle_x))),
                           (float)(Math.Sin(MathHelper.ToRadians(angle_y))),
                           (float)(Math.Cos(MathHelper.ToRadians(angle_x))));
            dir.Normalize();
            return dir;
        }

        public static Vector2 DirFromAngle2D(float angle_x)
        {
            Vector2 dir = new Vector2(
                           (float)(Math.Sin(MathHelper.ToRadians(angle_x))),
                           (float)(Math.Cos(MathHelper.ToRadians(angle_x))));
            dir.Normalize();
            return dir;
        }

        public static Vector3 DirFromAngle3D(float angle_x)
        {
            Vector3 dir = new Vector3(
                           (float)(Math.Sin(MathHelper.ToRadians(angle_x))),
                           0f,
                           (float)(Math.Cos(MathHelper.ToRadians(angle_x))));
            dir.Normalize();
            return dir;
        }

        public static float AngleFromDir(float x, float y)
        {
            return MathHelper.ToDegrees((float)(Math.Atan2(x, y)));
        }

        public static Matrix CreateRotation(float angle_x, float angle_y)
        {
            Matrix rotation = Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(angle_x),
                                                            -MathHelper.ToRadians(angle_y), 0f);
            return rotation;
        }

        public static Rectangle ShrinkRectangle(Rectangle rect, float by_x, float by_y)
        {
            rect.X += (int)(by_x * 0.5f);
            rect.Y += (int)(by_y * 0.5f);

            rect.Width -= (int)(by_x);
            rect.Height -= (int)(by_y);

            return rect;
        }

        public static int BytesToInt(byte[] arg)
        {
            return System.BitConverter.ToInt32(arg, 0);
        }

        public static byte BytesToByte(byte[] b)
        {
            return b[0];
        }

        public static byte[] ByteToBytes(byte b)
        {
            return new byte[1] { b };
        }

        public static byte[] IntToBytes(int arg)
        {
            return System.BitConverter.GetBytes(arg);
        }
    }
}
