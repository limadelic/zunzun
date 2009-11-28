namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(Settings.UserName)
            && !string.IsNullOrEmpty(Settings.Password)
        ;}}

        public void UseCredentials() {
            Domain.Settings.UserName = Settings.UserName;
            Domain.Settings.Password = Settings.Password;
        }
    }
}