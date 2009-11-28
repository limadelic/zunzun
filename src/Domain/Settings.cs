using System.Collections.Generic;

namespace Zunzun.Domain {

    public static class Settings {
    
        public static string UserName = "kinobot";
        public static string Password = "kashmir";
        
        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}