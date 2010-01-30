using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain.Classes;
using Zunzun.Utils;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Domain {

    public static class Settings {

        static KeyMaker KeyMaker { get { return Utils.ObjectFactory.NewKeyMaker; } }

        public static string UserName
        {
            get
            {
                return UtilsSettings.Default.UserName;
            }
            set
            {
                UtilsSettings.Default.UserName = value;
                Save();
            }
        }

        private static void Save()
        {
            UtilsSettings.Default.Save();
        }

        public static string Password
        {
            get
            {
                return string.IsNullOrEmpty(UtilsSettings.Default.Password) ? string.Empty : KeyMaker.Decrypt(UtilsSettings.Default.Password);
            }
            set
            {
                UtilsSettings.Default.Password = string.IsNullOrEmpty(value) ? string.Empty : KeyMaker.Encrypt(value);
                Save();
            }
            
        }
        
        public static string UrlShrinker
        {
            get
            {
                return UtilsSettings.Default.UrlShrinker;
            }
            set
            {
                UtilsSettings.Default.UrlShrinker = value;
                Save();
            }
        }
        
        public static string PhotoService
        {
            get
            {
                return UtilsSettings.Default.PhotoService;
            }
            set
            {
                UtilsSettings.Default.PhotoService = value;
                Save();
            }
        }
        
        public static readonly List<string> PhotoServices = new List<string> {"twitpic", "yfrog"};
        
        public static readonly List<string> UrlShrinkers = UrlShrinkerClass.Services.Keys.ToList();

        public const int NumberOfTweetsPerRequest = 100;

        public const string TwitterUrl = "http://www.twitter.com/";

        public const string MentionPreffix = "@";

        public static readonly List<string> AcceptedProtocols = new List<string> { "http", "https", "ftp" };
        public const string SmallPicSuffix = "_normal";
    }
}