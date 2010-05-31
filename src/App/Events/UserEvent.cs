using System.Windows;

namespace Zunzun.App.Events {
    public abstract class UserEvent {
        
        public static void Trigger(string UserName, object Source, RoutedEvent Event) {
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