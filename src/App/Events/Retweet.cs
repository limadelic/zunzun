using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Events {

    public class Retweet {
    
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("Retweet", RoutingStrategy.Bubble,
            typeof (RoutedEventHandler), typeof (object));

        public static void To(Tweet Tweet, object Source) {
            TweetEvent.Trigger(Tweet, Source, Event);
        }
    }
}