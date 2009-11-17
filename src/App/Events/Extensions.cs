using System.Windows;

namespace Zunzun.App.Events {

    public static class Extensions {
        
        public static void RaiseEvent(this object Source, RoutedEventArgs Args) {
            if (Source is UIElement) (Source as UIElement).RaiseEvent(Args);
            else (Source as ContentElement).RaiseEvent(Args);
        }
    }
}