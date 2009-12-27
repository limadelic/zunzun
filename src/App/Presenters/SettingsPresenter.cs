using Zunzun.App.Model;
using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public class SettingsPresenter {
    
        public SettingsView View { get; set; }
        public UserAuthenticator UserAuthenticator { get; set; }
        
        public void Load() {
            View.UrlShrinkers = Domain.Settings.UrlShrinkers;
            View.UrlShrinker = Domain.Settings.UrlShrinker;
            View.UserName = Domain.Settings.UserName;
            View.Password = Domain.Settings.Password;
        }

        public void Apply() {
            ApplyServicesSettings();
            Login();
        }

        public virtual void Login() {
            try {

                UserAuthenticator.Authenticate(View.UserName, View.Password);
                View.Close();

            } catch { View.ShowError(); }
        }

        public virtual void ApplyServicesSettings() {
            Domain.Settings.UrlShrinker = View.UrlShrinker;
        }
    }
}