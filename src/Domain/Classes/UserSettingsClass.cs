using Zunzun.Utils;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Domain.Classes {

    public class UserSettingsClass : UserSettings {

        public KeyMaker KeyMaker { get; set; }
    
        public void Save() {

            UtilsSettings.Default.UserName = Settings.UserName;
            UtilsSettings.Default.Password = string.IsNullOrEmpty(Settings.Password) ? 
                string.Empty : KeyMaker.Encrypt(Settings.Password);
            
            UtilsSettings.Default.UrlShrinker = Settings.UrlShrinker;
            UtilsSettings.Default.PhotoService = Settings.PhotoService;
            
            Write();
        }

        public void Load() { 

            Settings.UserName = UtilsSettings.Default.UserName;
            Settings.Password = string.IsNullOrEmpty(UtilsSettings.Default.Password) ? 
                string.Empty : KeyMaker.Decrypt(UtilsSettings.Default.Password);
            
            Settings.UrlShrinker = UtilsSettings.Default.UrlShrinker;
            Settings.PhotoService = UtilsSettings.Default.PhotoService;
        }

        public virtual void Write() { UtilsSettings.Default.Save(); }
    }
}