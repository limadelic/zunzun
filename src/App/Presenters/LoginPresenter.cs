using Zunzun.App.Model;
using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public class LoginPresenter {
    
        public LoginView View { get; set; }
        public UserAuthenticator UserAuthenticator { get; set; }
        
        public void Login() {
            UserAuthenticator.Authenticate(View.UserName, View.Password);
            View.Close();
        }
    }
}