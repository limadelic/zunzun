using System.Collections.Generic;
using System.Linq;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {
    
        public List<Tweet> Tweets { get { return 
            Request(HomeSpec)
        ;}}

        public List<Tweet> TweetsSince(long Id) { return 
            Request(TweetsSinceSpec(Id))
        ;}

        public List<Tweet> TweetsBy(User User) { return 
            Request(TweetsByUserNameSpec(User.UserName))
        ;}

        public void UpdateStatus(Tweet Tweet) {
            UpdateStatusSpec(Tweet.Content).Request()
        ;}

        public void SendReply(Tweet Tweet, long AssociatedTweetId) {
            ReplyStatusSpec(Tweet.Content, AssociatedTweetId).Request();
        }


        ITwitterStatuses Statuses { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Statuses()
        ;}}

        ITwitterHomeTimeline Home { get { return 
            Statuses.OnHomeTimeline()
        ;}}

        ITwitterUserTimeline User { get { return 
            Statuses.OnUserTimeline()
        ;}}

        List<Tweet> Request(ITwitterLeafNode Spec) { return 
            Spec.Request().ToTweets().ToList()
        ;}

        #region Specs

        public virtual ITwitterLeafNode HomeSpec { get { return 
            Home.Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}

        public virtual ITwitterLeafNode TweetsSinceSpec(long Id) { return 
            Home.Since(Id).AsJson()
        ;}

        public virtual ITwitterLeafNode TweetsByUserNameSpec(string UserName) { return 
            User.For(UserName).AsJson()
        ;}

        ITwitterLeafNode UpdateStatusSpec(string Content) { return 
            Statuses.Update(Content).AsJson()
        ;}

        ITwitterLeafNode ReplyStatusSpec(string Content, long Id) { return 
            Statuses.Update(Content)
            .InReplyToStatus(Id).AsJson()
        ;}

        #endregion
    }
}