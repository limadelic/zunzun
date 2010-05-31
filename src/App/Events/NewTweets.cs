using System.Collections.Generic;
using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Events {
    
    public class NewTweets {
    
        public static readonly RoutedEvent Event = EventManager.RegisterRoutedEvent("NewTweets",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (object));

        public static void Found(List<Tweet> Tweets, object Source) {
            TweetsEvent.Trigger(Tweets, Source, Event);
        }
    }
}