using System.Collections.Generic;

namespace Zunzun.Domain {

    public static class Settings {
    
        static Settings() {
//            UserName = "kinobot";
//            Password = "kashmir";
        }

        public static string UserName {
            get { return Properties.Settings.Default.UserName;  }
            set { Properties.Settings.Default.UserName = value; }
        }
        
        public static string Password {
            get { return Properties.Settings.Default.Password;  }
            set { Properties.Settings.Default.Password = value; }
        }
        
        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}