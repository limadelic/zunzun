using System.Windows;

namespace Zunzun.App.Events {

    public class ShowUserHome {
        
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("ShowUserHome",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(object));

        public static void Of(string UserName, object Source) {
            var Args = new Args(Event, Source, UserName);
            Source.RaiseEvent(Args);
        }
        
        public class Args : RoutedEventArgs {
        
            public string UserName;

            public Args(RoutedEvent RoutedEvent, object Source, string UserName)
                : base(RoutedEvent, Source) { this.UserName = UserName; }
        }
    }
}