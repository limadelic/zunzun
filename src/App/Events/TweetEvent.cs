using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Events {

    public abstract class TweetEvent {
        
        public static void Trigger(Tweet Tweet, object Source, RoutedEvent Event) {
            var Args = new Args(Event, Source, Tweet);
            Source.RaiseEvent(Args);
        }

        public class Args : RoutedEventArgs {
            public Tweet Tweet;

            public Args(RoutedEvent RoutedEvent, object Source, Tweet Tweet) : 
                base(RoutedEvent, Source) { this.Tweet = Tweet; }
        }
    }
}