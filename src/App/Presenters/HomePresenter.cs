using System.Collections.Generic;
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
            View.Tweets.Clear();
            TweetService.Tweets.ForEach(View.Tweets.Add);
        }

        public virtual void SetupTimer() {
            Timer.Notify += CheckForNewTweets;
            Timer.NotifyEvery(Settings.DefaultRefreshCycle);
        }

        Tweet LatestTweet { get { return View.Tweets[0]; }}

        public void CheckForNewTweets() {
            var NewTweets = TweetService.TweetsSince(LatestTweet.Id);
            Add(NewTweets);
        }

        public void Add(List<Tweet> NewTweets) {
            View.Tweets.InsertAtTop(NewTweets);
        }
    }
}