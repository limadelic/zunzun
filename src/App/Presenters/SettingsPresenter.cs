using Zunzun.App.Model;
using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public class SettingsPresenter {
    
        public SettingsView View { get; set; }
        public UserAuthenticator UserAuthenticator { get; set; }
        
        public void Login() {
            try {

                UserAuthenticator.Authenticate(View.UserName, View.Password);
                View.Close();

            } catch { View.ShowError(); }
        }

        public void Load() {
            View.UrlShrinkers = Domain.Settings.UrlShrinkers;
        }
    }
}