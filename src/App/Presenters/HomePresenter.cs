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
        public bool InConversationMode { get; set; }

        public void Load() {
            Show();
            SetupTimer();
        }

        public void Add(List<Tweet> TweetsToAdd) {
            View.Tweets.InsertAtTop(TweetsToAdd);
        }        

        public void Show() {
            View.Tweets.Clear();
            Add(Tweets);
        }

        public virtual void SetupTimer() {
            Timer.Notify += CheckForNewTweets;
            Timer.NotifyEvery(Settings.DefaultRefreshCycle);
        }

        Tweet LatestTweet { get { return Tweets[0]; }}

        public virtual void CheckForNewTweets() {
            var NewTweets = TweetService.TweetsSince(LatestTweet.Id);

            if (InConversationMode && NewTweets.Any()) Show();
            else Add(NewTweets);
        }

        public void ShowConversation(Tweet Tweet)
        {
            View.Tweets.Clear();
            InConversationMode = true;
            Add(ConstructConversation(Tweet));
        }

        List<Tweet> ConstructConversation(Tweet Tweet)
        {
            return Domain.ObjectFactory.NewConversation(TweetService.Tweets)
                .ConstructConversation(Tweet);
        }
    }
}