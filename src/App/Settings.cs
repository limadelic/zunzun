using System.ComponentModel;
using System.Windows;

namespace Zunzun.App {

    public static class Settings {

        public static string UserName {
            get { return Domain.Settings.UserName;  }
            set { Domain.Settings.UserName = value; }
        }
        
        public static string Password {
            get { return Domain.Settings.Password;  }
            set { Domain.Settings.Password = value; }
        }
        
        public const string InfoBrush = "InfoBrush";
        public const string ContentBrush = "ContentBrush";
        
        public static int DefaultRefreshCycle = 60 * 1000;

        public static readonly bool IsInDesignMode = 
            DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public static string CurrentUrlShrinker = "tinyurl";
    }
}