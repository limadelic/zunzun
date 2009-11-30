namespace Zunzun.App.Views {

    public interface LoginView {
    
        string UserName { get; set; }
        string Password { get; set; }
        
        void Close();
        void ShowError();
    }
}