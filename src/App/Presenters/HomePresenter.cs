using Zunzun.App.Model;
using Zunzun.App.Views;
using Zunzun.Domain;

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
            TweetService.Tweets.ForEach(View.Tweets.Add);
        }

        public virtual void SetupTimer() {
            Timer.Notify += CheckForNewTweets;
            Timer.NotifyEvery(Settings.DefaultRefreshCycle);
        }

        public void CheckForNewTweets() {
            TweetService.NewTweets.ForEach(View.Tweets.Add);
        }
    }
}