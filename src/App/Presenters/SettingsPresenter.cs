using Zunzun.App.Model;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class SettingsPresenter {
    
        public SettingsView View { get; set; }
        public UserAuthenticator UserAuthenticator { get; set; }
        public UserSettings UserSettings { get; set; }

        public void Load() {
            View.UrlShrinkers = Domain.Settings.UrlShrinkers;
            View.UrlShrinker = Domain.Settings.UrlShrinker;
            View.PhotoService = Domain.Settings.PhotoService;
            View.PhotoServices = Domain.Settings.PhotoServices;
            View.UserName = Domain.Settings.UserName;
            View.Password = Domain.Settings.Password;
        }

        public void Apply() {
            ApplyServicesSettings();
            Login();
            UserSettings.Save();
        }

        public virtual void Login() {
            try {

                UserAuthenticator.Authenticate(View.UserName, View.Password);
                View.Close();

            } catch { View.ShowError(); }
        }

        public virtual void ApplyServicesSettings() {
            Domain.Settings.UrlShrinker = View.UrlShrinker;
            Domain.Settings.PhotoService = View.PhotoService;
        }
    }
}