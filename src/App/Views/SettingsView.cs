using System.Collections.Generic;

namespace Zunzun.App.Views {

    public interface SettingsView {
    
        string UserName { get; set; }
        string Password { get; set; }

        string UrlShrinker { get; set; }
        List<string> UrlShrinkers { get; set; }
        
        string PhotoService { get; set; }
        List<string> PhotoServices { get; set; }

        void Close();
        void ShowError();
    }
}