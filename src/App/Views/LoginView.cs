namespace Zunzun.App.Views {

    public interface LoginView {
    
        string UserName { get; }
        string Password { get; }
        
        void Close();
        void ShowError();
    }
}