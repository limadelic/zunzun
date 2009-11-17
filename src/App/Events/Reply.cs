using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Events {

    public class Reply {
        
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("Reply",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(object));

        public static void To(Tweet Tweet, object Source) {
            var Args = new Args(Event, Source, Tweet);
            Source.RaiseEvent(Args);
        }

        public class Args : RoutedEventArgs {
        
            public Tweet Tweet;

            public Args(RoutedEvent RoutedEvent, object Source, Tweet Tweet)
                : base(RoutedEvent, Source) { this.Tweet = Tweet; }
        }
    }
}