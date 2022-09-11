using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Iris
{
    public class GuiManager
    {
        private TypeButton type_button = null;
        private TypeCheckBox type_cb = null;
        
        private SceneManager manager;

        private float layer_depth = 0.5f;

        public GuiManager(SceneManager manager)
        {
            this.manager = manager;

            /*TouchPanel.EnabledGestures = GestureType.Tap;

            
             bool tapGesture = false;
			while (TouchPanel.IsGestureAvailable)
			{
				GestureSample sample = TouchPanel.ReadGesture();
				if (sample.GestureType == GestureType.Tap)
				{
                    
					tapGesture = true;
				}
			}*/
             
        }

        public bool AddResourceButton(Texture2D texture, SpriteFont font, float scale)
        {
            if(texture == null || font == null || scale < 0.0f)
                return false;

            // Original size, then scale it
            float size_w = (200.0f / 800.0f) * scale;
            float size_h = (60.0f / 480.0f) * scale;

            float width = (float)manager.Width * size_w;
            float height = (float)manager.Height * size_h;

            width = 300f;
            height = 90f;

            type_button = new TypeButton(texture, font, width, height); 

            return true;
        }

        public bool AddResourceCheckBox(Texture2D t_background, Texture2D t_check, float scale)
        {
            if (t_background == null || t_check == null || scale < 0.0f)
                return false;

            // Original size, then scale it
            float size_w = 80f * scale;
            float size_h = 80f * scale;

            float width = (float)manager.Width * size_w;
            float height = (float)manager.Height * size_h;

            type_cb = new TypeCheckBox(t_background, t_check, size_w, size_h);

            return true;
        }

        public float GetScaledWidth(ElementType type)
        {
            return (float)(type.Width / manager.Width) * 100f;
        }

        public float GetScaledHeight(ElementType type)
        {
            return (float)(type.Height / manager.Height) * 100f;
        }

        public SceneManager Manager
        {
            get { return manager; }
        }

        public TypeButton GetTypeButton
        {
            get { return type_button; }
        }

        public TypeCheckBox GetTypeCheckBox
        {
            get { return type_cb; }
        }

        public float LayerDepth
        {
            get { return layer_depth; }
        }
    }
}
