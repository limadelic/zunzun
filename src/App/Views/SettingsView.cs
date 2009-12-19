using System.Collections.Generic;

namespace Zunzun.App.Views {

    public interface SettingsView {
    
        string UserName { get; }
        string Password { get; }
        List<string> UrlShrinkers { set; }

        void Close();
        void ShowError();
    }
}