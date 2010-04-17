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

        public void ShowConversation(Tweet tweet)
        {
            View.Tweets.Clear();
            Add(ConstructConversation(tweet));
        }

        List<Tweet> ConstructConversation(Tweet tweet)
        {
            return Domain.ObjectFactory.NewConversation(TweetService.Tweets)
                .ConstructConversation(tweet);
        }
    }
}