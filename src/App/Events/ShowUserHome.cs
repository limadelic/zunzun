using System.Windows;
using Zunzun.App.Views.Xaml;

namespace Zunzun.App.Events {

    public class ShowUserHome {
        
        public static readonly RoutedEvent ShowUserHomeEvent = EventManager.RegisterRoutedEvent("ShowUserHomeEvent",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Main));

        public static void RaiseCommandEvent(string UserName, object Source) {
            if (UserName == null) return;
            var EventArgs = new ShowUserHomeEventArgs(ShowUserHomeEvent, Source, UserName);
            ((ContentElement) Source).RaiseEvent(EventArgs);
        }

        public class ShowUserHomeEventArgs : RoutedEventArgs {
        
            public string UserName;

            public ShowUserHomeEventArgs(RoutedEvent RoutedEvent, object Source, string UserName)
                : base(RoutedEvent, Source) { this.UserName = UserName; }
        }
    }
}