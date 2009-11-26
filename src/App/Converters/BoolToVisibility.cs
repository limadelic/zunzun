using System.Windows;

namespace Zunzun.App.Converters {

    public static class BoolToVisibility {
        
        public static void IsVisibleIf(this UIElement Control, bool Value) {
            Control.Visibility = Value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}