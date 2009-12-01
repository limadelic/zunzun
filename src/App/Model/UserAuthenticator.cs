namespace Zunzun.App.Model {

    public interface UserAuthenticator {
    
        bool HasCredentials { get; }
        void Authenticate(string UserName, string Password);
        void UseCredentials();
    }
}