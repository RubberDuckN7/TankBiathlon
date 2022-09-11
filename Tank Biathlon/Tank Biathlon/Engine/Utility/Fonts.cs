using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Tank_Biathlon
{
    static class Fonts
    {
        static SpriteFont font_menu;
        static SpriteFont font_message;
        static SpriteFont font_score;

        public static SpriteFont FontMenu
        {
            get { return font_menu; }
        }

        public static SpriteFont FontMessage
        {
            get { return font_message; }
        }

        public static SpriteFont FontScore
        {
            get { return font_score; }
        }

        public static void LoadContent(ContentManager content)
        {
            font_menu = content.Load<SpriteFont>("DefaultFont");
            font_message = content.Load<SpriteFont>("DefaultFont");
            font_score = content.Load<SpriteFont>("ScoreFont");
        }
    }
}
