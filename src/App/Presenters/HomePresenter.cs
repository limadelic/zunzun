using System.Collections.Generic;
using System.Linq;
using Zunzun.App.Model;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Utils;

namespace Zunzun.App.Presenters {
    
    public class HomePresenter {
    
        public HomeView View { get; set; }
        public TweetService TweetService { get; set; }
        public Timer Timer { get; set; }
        public virtual List<Tweet> Tweets { get { return TweetService.Tweets; }}

        public void Load() {
            Show();
            SetupTimer();
        }

        public void Add(List<Tweet> Tweets) {
            View.Tweets.InsertAtTop(Tweets);
        }        

        public virtual void Show() {
            View.Tweets.Clear();
            Add(Tweets);
        }

        public virtual void SetupTimer() {
            Timer.Notify += CheckForNewTweets;
            Timer.NotifyEvery(Settings.DefaultRefreshCycle);
        }

        Tweet LatestTweet { get { return Tweets[0]; }}

        public virtual void CheckForNewTweets() {
            Add(TweetService.TweetsSince(LatestTweet.Id));
        }

        Tweet rootTweet { get; set; }

        public void ShowConversation(Tweet tweet)
        {
            View.Tweets.Clear();
            Add(ConstructConversation(tweet));
        }

        public virtual List<Tweet> ConstructConversation(Tweet tweet)
        {
            rootTweet = RootTweet(tweet);
            return Tweets.Where(Tweet => ConversationIds.Contains(Tweet.Id)).ToList();
        }

        protected List<long> ConversationIds
        {
            get
            {
                var list = new List<long> { rootTweet.Id };
                ConversationIdsHelper(rootTweet, list);
                return list;
            }
        }

        private void ConversationIdsHelper(Tweet currentTweet, ICollection<long> ids)
        {
            var children = Tweets.Where(tweet => tweet.ReplyTo == currentTweet.Id);

            if (children.Count() == 0) return;

            children.ForEach(tweet =>
            {
                ids.Add(tweet.Id);
                ConversationIdsHelper(tweet, ids);
            });
        }

        protected Tweet RootTweet(Tweet tweet)
        {
            var currentTweet = tweet;

            while (currentTweet.ReplyTo > 0)
            {
                var tempTweet = currentTweet;
                currentTweet = Tweets.Where(x => x.Id == tempTweet.ReplyTo).First();
            }

            return currentTweet;
        }
    }
}