using System.Windows;

namespace Zunzun.App.Controls {

    public static class Extensions {
        
        public static void Show(this UIElement Control) {
            Control.Visibility = Visibility.Visible;
        }

        public static void Hide(this UIElement Control) {
            Control.Visibility = Visibility.Hidden;
        }
    }
}