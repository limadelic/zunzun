using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {
        
        List<Tweet> tweets;
        public List<Tweet> Tweets { get { return tweets ?? InitTweets; }}
        List<Tweet> InitTweets { get { return tweets = Request(HomeSpec); }}

        public List<Tweet> TweetsSince(long Id) { 
            return Request(TweetsSinceSpec(Id));
        }

        public void UpdateStatus(Tweet Tweet) {
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses()
            .Update(Tweet.Content)
            .AsJson().Request();
        }
        
        ITwitterHomeTimeline Home { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses().OnHomeTimeline()
        ;}}

        List<Tweet> Request(ITwitterLeafNode Spec) {
            return Spec.Request().ToTweets();
        }

        public virtual ITwitterLeafNode HomeSpec { get { return 
            Home.Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}
        
        public virtual ITwitterLeafNode TweetsSinceSpec(long Id) { return 
            Home.Since(Id).AsJson()
        ;}
    }
}