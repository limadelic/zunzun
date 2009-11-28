namespace Zunzun.App {

    public static class Settings {

        public const string InfoBrush = "InfoBrush";
        public const string ContentBrush = "ContentBrush";
        
        public static int DefaultRefreshCycle = 60 * 1000;

        public static readonly bool IsInDesignMode =
            System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    
        public static string UserName { 
            get { return Properties.Settings.Default.UserName; }
            set { Properties.Settings.Default.UserName = value; }
        }
        
        public static string Password { 
            get { return Properties.Settings.Default.Password; }
            set { Properties.Settings.Default.Password = value; }
        }
    }
}