namespace Zunzun.App.Views {

    public interface SettingsView {
    
        string UserName { get; }
        string Password { get; }
        
        void Close();
        void ShowError();
    }
}