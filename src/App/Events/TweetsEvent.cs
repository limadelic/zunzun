using System.Collections.Generic;
using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Events {

    public abstract class TweetsEvent {
        
        public static void Trigger(List<Tweet> Tweets, object Source, RoutedEvent Event) {
            var Args = new Args(Event, Source, Tweets);
            Source.RaiseEvent(Args);
        }

        public class Args : RoutedEventArgs {
            public List<Tweet> Tweets;

            public Args(RoutedEvent RoutedEvent, object Source, List<Tweet> Tweets) : 
                base(RoutedEvent, Source) { this.Tweets = Tweets; }
        }
    }
}