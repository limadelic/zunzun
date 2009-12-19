using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain.Classes;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Domain {

    public static class Settings {
    
        static Settings() {
        
            UserName = UtilsSettings.Default.UserName;    
            Password = UtilsSettings.Default.Password;
                
            UrlShrinker = !string.IsNullOrEmpty(UtilsSettings.Default.UrlShrinker) ?
                UtilsSettings.Default.UrlShrinker : "u.nu";    
        }

        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string UrlShrinker { get; set; }
        
        public static readonly List<string> UrlShrinkers = 
            UrlShrinkerClass.Services.Keys.ToList();

        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://www.twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}