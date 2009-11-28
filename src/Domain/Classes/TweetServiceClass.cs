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

        List<Tweet> Request(ITwitterLeafNode Spec) { return 
            Spec.Request().ToTweets()
        ;}

        #region Specs

        public virtual ITwitterLeafNode HomeSpec { get { return 
            TwitterAPI.HomeStatuses.Take(Settings.NumberOfTweetsPerRequest).AsJson()
        ;}}

        public virtual ITwitterLeafNode TweetsSinceSpec(long Id) { return 
            TwitterAPI.HomeStatuses.Since(Id).AsJson()
        ;}

        public virtual ITwitterLeafNode TweetsByUserNameSpec(string UserName) { return 
            TwitterAPI.UserStatuses.For(UserName).AsJson()
        ;}

        ITwitterLeafNode UpdateStatusSpec(string Content) { return 
            TwitterAPI.Statuses.Update(Content).AsJson()
        ;}

        ITwitterLeafNode ReplyStatusSpec(string Content, long Id) { return 
            TwitterAPI.Statuses.Update(Content)
            .InReplyToStatus(Id).AsJson()
        ;}

        #endregion
    }
}