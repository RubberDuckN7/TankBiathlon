using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Tank_Biathlon
{
    static class SoundManager
    {
        static float volume = 0.2f;
        static bool sound_off = false;
        static bool music_off = false;

        static SoundEffectInstance inst_blowup;
        static SoundEffectInstance inst_score;
        static SoundEffectInstance inst_theme;

        public static float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                if (inst_blowup != null)
                {
                    inst_blowup.Volume = volume;
                    inst_score.Volume = volume;
                    inst_theme.Volume = volume;
                }
            }
        }

        public static bool SoundOff
        {
            get { return sound_off; }
            set { sound_off = value; }
        }

        public static bool MusicOff
        {
            get { return music_off; }
            set { music_off = value; }
        }

        public static void StartMusic()
        {
            if (music_off)
                return;

            inst_theme.Play();
        }

        public static void StopMusic()
        {
            inst_theme.Stop();
        }

        public static void PlayOnBlowup()
        {
            if (sound_off)
                return;
            inst_blowup.Play();
        }

        public static void PlayOnScore()
        {
            if (sound_off)
                return;

            inst_score.Play();
        }

        public static void LoadContent(ContentManager content)
        {
            SoundEffect blowup = content.Load<SoundEffect>("hit_target");
            SoundEffect score = content.Load<SoundEffect>("score_scene");
            SoundEffect theme_looped = content.Load<SoundEffect>("play_theme");

            inst_blowup = blowup.CreateInstance();
            inst_score = score.CreateInstance();
            inst_theme = theme_looped.CreateInstance();

            inst_blowup.IsLooped = false;
            inst_score.IsLooped = false;
            inst_theme.IsLooped = true;

            inst_blowup.Volume = volume;
            inst_score.Volume = volume;
            inst_theme.Volume = volume;
        }

        public static void StopAll()
        {
            if (inst_theme != null)
                inst_theme.Stop();
        }
    }
}
