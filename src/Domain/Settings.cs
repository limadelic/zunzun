using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain.Classes;
using Zunzun.Utils.Aspects;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Domain {

    public static class Settings {
    
        [UserSetting]
        public static string UserName { get; set; }
        
        [UserSetting, Encrypted]
        public static string Password { get; set; }
        
        [UserSetting]
        public static string UrlShrinker { get; set; }
        
        public static string ImageUploader { get; set; }
        
        public static readonly List<string> UrlShrinkers = 
            UrlShrinkerClass.Services.Keys.ToList();

        public static readonly List<string> ImageUploaders =
            new List<string>();

        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://www.twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}