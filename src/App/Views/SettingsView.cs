using System.Collections.Generic;

namespace Zunzun.App.Views {

    public interface SettingsView {
    
        string UserName { get; set; }
        string Password { get; set; }

        List<string> UrlShrinkers { set; }
        string UrlShrinker { get; set; }

        void Close();
        void ShowError();
    }
}