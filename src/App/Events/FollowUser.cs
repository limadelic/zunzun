using System.Windows;

namespace Zunzun.App.Events {

    public class FollowUser {
        
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("FollowUser",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(object));

        public static void Of(string UserName, object Source) {
            UserEvent.Trigger(UserName, Source, Event);
        }
    }
}