using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {
    
    public class TweetServiceClass : TweetService {
        
        public List<Tweet> Tweets { get {
            return Home.Request().ToTweets()
        ;}}

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
    }
}