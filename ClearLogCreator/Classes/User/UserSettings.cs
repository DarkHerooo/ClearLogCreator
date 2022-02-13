using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes.User
{
    public class UserSettings
    {
        public string MinecraftLogsFolder;
        public string MyLogsFolder;
        public bool DeleteUselessFilesInMinecraftFolder;
        public bool DeleteUselessFilesInMyFolder;
    }
}
