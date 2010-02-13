using System.Collections.Generic;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Fluent;
using Dimebrain.TweetSharp.Model;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class TweetServiceClass : TweetService {
        
        public List<Tweet> Tweets { get { return Request(HomeSpec); }}

        public List<Tweet> TweetsSince(long Id) { return 
            Request(TweetsSinceSpec(Id))
        ;}

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

        static List<Tweet> Search(ITwitterLeafNode Spec) {
            var aa = Spec.Request();
            return aa.AsSearchResult().ToTweets()
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

        public virtual TwitterSearchResult TweetsFoundFor(string SearchText) { return
            TweetsContainingSpec(SearchText).Request().AsSearchResult()            
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