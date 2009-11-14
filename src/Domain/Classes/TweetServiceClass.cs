using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

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

        public virtual ITwitterLeafNode Home { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
            .Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}
        
        public virtual List<Tweet> NewTweets { get {
            return TweetsSinceLastOne.Request().ToTweets();
        }}

        public virtual ITwitterLeafNode TweetsSinceLastOne { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
            .Since(Tweets[0].Id).AsJson()
        ;}}
    }
}