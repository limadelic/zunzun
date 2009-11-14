using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {
        
        public List<Tweet> Tweets { get { return Request(HomeSpec); }}

        public List<Tweet> TweetsSince(long Id) { 
            return Request(TweetsSinceSpec(Id));
        }

        public void UpdateStatus(Tweet Tweet) {
            Statuses.Update(Tweet.Content)
            .AsJson().Request();
        }

        ITwitterStatuses Statuses { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses()
        ;}}

        ITwitterHomeTimeline Home { get { return 
            Statuses.OnHomeTimeline()
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