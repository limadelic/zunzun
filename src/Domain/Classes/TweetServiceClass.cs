using System.Collections.Generic;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;
using Zunzun.Utils;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {

        List<Tweet> tweets;
        public virtual List<Tweet> Tweets { get { return tweets ?? InitTweets; }}
        List<Tweet> InitTweets { get { return tweets = Request(HomeSpec); }}

        public List<Tweet> TweetsSince(long Id) {
            var Results = Request(TweetsSinceSpec(Id));
            Tweets.InsertAtTop(Results);
            return Results;
        }

        public List<Tweet> TweetsBy(User SpecificUser) { return 
            Request(TweetsByUserNameSpec(SpecificUser.UserName))
        ;}

        public List<Tweet> TweetsContaining(string SearchText) { return 
            Search(TweetsContainingSpec(SearchText))
        ;}

        public void UpdateStatus(Tweet Tweet) {
            UpdateStatusSpec(Tweet.Content).Request()
        ;}

        public void SendReply(Tweet Tweet) {
            ReplyStatusSpec(Tweet.Content, Tweet.ReplyTo).Request()
        ;}

        static List<Tweet> Request(ITwitterLeafNode Spec) { return 
            Spec.Request().AsStatuses().ToTweets()
        ;}

        static List<Tweet> Search(ITwitterLeafNode Spec) { return
            Spec.Request().AsSearchResult().ToTweets()
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

        public virtual ITwitterLeafNode TweetsContainingSpec(string SearchText) { return
            TwitterAPI.Request.Search().Query().Containing(SearchText).AsJson()            
        ;}

        static ITwitterLeafNode UpdateStatusSpec(string Content) { return
            TwitterAPI.Statuses.Update(Content).AsJson()
        ;}

        static ITwitterLeafNode ReplyStatusSpec(string Content, long Id) { return 
            TwitterAPI.Statuses.Update(Content).InReplyToStatus(Id).AsJson()
        ;}

        #endregion
    }
}