using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes.User
{
    public static class User
    {
        public static UserSettings UserSettings { get; set; }

        private static string _fileName = "../../Data/user_settings.json";
        public static void SetUser()
        {
            if (File.Exists(_fileName)) 
                UserSettings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(_fileName));
            else
            {
                UserSettings = new UserSettings();
                UserSettings.MinecraftLogsFolder = 
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\logs";
                UserSettings.CopyFilesFromMinecraftFolder = true;
                UserSettings.HideUselessFiles = false;
            }
        }

        public static void SaveUser()
        {
            File.WriteAllText(_fileName, JsonConvert.SerializeObject(UserSettings));
        }
    }
}
