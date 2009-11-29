namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(Settings.UserName)
            && !string.IsNullOrEmpty(Settings.Password)
        ;}}
    }
}