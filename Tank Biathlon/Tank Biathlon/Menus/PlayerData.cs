using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.IsolatedStorage;

namespace Tank_Biathlon
{
    static class PlayerData
    {
        public static byte Version = 1;
        public static bool MusicOn = true;
        public static bool SoundOn = true;

        public static class Save
        {
            public static bool FileExist(string file)
            {
                using (IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (fs.FileExists(file))
                    {
                        return true;
                    }
                }

                return false;
            }

            public static void SaveFile(string file)
            {
                IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();

                IsolatedStorageFileStream fs = null;
                using (fs = savegameStorage.CreateFile(file))
                {
                    if (fs != null)
                    {
                        fs.WriteByte(PlayerData.Version);
                        if (PlayerData.SoundOn)
                            fs.WriteByte(1);
                        else
                            fs.WriteByte(0);

                        if (PlayerData.MusicOn)
                            fs.WriteByte(1);
                        else
                            fs.WriteByte(0);
                    }
                }
            }

            public static void LoadFile(string file)
            {
                using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(file, System.IO.FileMode.Open)) //, FileAccess.Read, FileShare.Read))
                    {
                        if (fs != null)
                        {
                            byte version;
                            byte sound;
                            byte music;

                            version = (byte)fs.ReadByte();
                            sound = (byte)fs.ReadByte();
                            music = (byte)fs.ReadByte();

                            PlayerData.Version = version;
                            PlayerData.SoundOn = (sound == (byte)1) ? true : false;
                            PlayerData.MusicOn = (music == (byte)1) ? true : false;
                        }
                    }
                }
            }
        }
    }
}
