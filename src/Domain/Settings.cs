using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain.Classes;

namespace Zunzun.Domain {

    public static class Settings {
    
        public static string UserName { get; set; }
        public static string Password { get; set; }

        public static string UrlShrinker = "u.nu";
        public static readonly List<string> UrlShrinkers = 
            UrlShrinkerClass.Services.Keys.ToList();

        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://www.twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}