using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public delegate void NewTweetsAreAvailable(List<Tweet> Tweets);
    
    public class TweetServiceClass : TweetService {
        
        List<Tweet> tweets;
        public List<Tweet> Tweets { get { return tweets ?? InitTweets; }}
        List<Tweet> InitTweets { get { return tweets = Home.Request().ToTweets();}}

        public void UpdateStatus(Tweet Tweet) {
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses()
            .Update(Tweet.Content)
            .AsJson().Request();
        }

        public void UpdateRefreshCycle(int RefreshCycle) {
            
        }

        public virtual ITwitterLeafNode Home { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
            .Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}
        
        public virtual List<Tweet> NewTweets { get { return null; }}
        
        public event NewTweetsAreAvailable NewTweetsAreAvailable;

        bool NoOneIsExpectingNewTweets { get { return NewTweetsAreAvailable == null; } }
        
        public void CheckForNewTweets() {
            if (NoOneIsExpectingNewTweets) return;
            
            var AvailableTweets = NewTweets;
            if (AvailableTweets.Count == 0) return;
            
            NewTweetsAreAvailable(AvailableTweets);
        }
    }
}