using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {
        
        List<Tweet> tweets;
        public List<Tweet> Tweets { get { return tweets ?? InitTweets; }}
        List<Tweet> InitTweets { get { return tweets = Request(Home); }}

        public virtual ITwitterLeafNode Home { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
            .Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}
        
        public void UpdateStatus(Tweet Tweet) {
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses()
            .Update(Tweet.Content)
            .AsJson().Request();
        }
        
        List<Tweet> Request(ITwitterLeafNode Spec) {
            return Spec.Request().ToTweets();
        }

        public List<Tweet> TweetsSince(long Id) { 
            return Request(TweetsSinceSpec(Id));
        }

        public virtual ITwitterLeafNode TweetsSinceSpec(long Id) { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
            .Since(Id).AsJson()
        ;}
    }
}