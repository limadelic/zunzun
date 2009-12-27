using System.ComponentModel;
using System.Windows;

namespace Zunzun.App {

    public static class Settings {

        public const string InfoBrush = "InfoBrush";
        public const string ContentBrush = "ContentBrush";
        
        public static int DefaultRefreshCycle = 60 * 1000;

        public static readonly bool IsInDesignMode = 
            DesignerProperties.GetIsInDesignMode(new DependencyObject());
    }
}