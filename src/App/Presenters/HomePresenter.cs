using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void Load() {
            Show();
            SetupTimer();
        }

        public virtual void Show() {
            View.HomeTweets.Clear();
            TweetService.Tweets.ForEach(View.HomeTweets.Add);
        }

        public virtual void SetupTimer() {
            Timer.Notify += CheckForNewTweets;
            Timer.NotifyEvery(Settings.DefaultRefreshCycle);
        }

        Tweet LatestTweet { get { return View.HomeTweets[0]; }}

        public void CheckForNewTweets() {
            var NewTweets = TweetService.TweetsSince(LatestTweet.Id);
            Add(NewTweets);
        }

        public void Add(List<Tweet> NewTweets) {
            View.HomeTweets.InsertAtTop(NewTweets);
        }

        Tweet rootTweet { get; set; }

        public void ShowConversation(Tweet tweet)
        {
            ConstructConversation(tweet);
            View.MakeConversationVisible();
        }

        private void ConstructConversation(Tweet tweet)
        {
            rootTweet = RootTweet(tweet);
            View.ConvoTweets = new ObservableCollection<Tweet>(
                View.HomeTweets.Where(Tweet => ConversationIds.Contains(Tweet.Id)).ToList()
            );
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
            var children = View.HomeTweets.Where(tweet => tweet.ReplyTo == currentTweet.Id);

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
                currentTweet = View.HomeTweets.Where(x => x.Id == tempTweet.ReplyTo).First();
            }

            return currentTweet;
        } 

    }
}