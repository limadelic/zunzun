namespace Zunzun.App.Model {

    public interface UserAuthenticator {
    
        bool HasCredentials { get; }
        void Authenticate(string Username, string Password);
    }
}