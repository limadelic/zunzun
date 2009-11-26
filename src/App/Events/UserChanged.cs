using System.Windows;

namespace Zunzun.App.Events {

    public class UserChanged {
        
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("UserChanged",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(object));

        public static void With(string UserName, object Source) {
            UserEvent.Trigger(UserName, Source, Event);
        }
    }
}