namespace Zunzun.App {

    public static class Settings {

        public const string InfoBrush = "InfoBrush";
        public const string ContentBrush = "ContentBrush";
        
        public static int DefaultRefreshCycle = 60 * 1000;

        public static readonly bool IsInDesignMode =
            System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
    }
}