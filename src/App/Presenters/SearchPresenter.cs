using System.Collections.Generic;
using System.Windows.Input;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class SearchPresenter {
    
        public SearchView View { get; set; }
        public TweetService TweetService { get; set; }

        public virtual void Search() {
            NotifyNewTweets(TweetService.TweetsContaining(View.SearchText));
        }

        public virtual void NotifyNewTweets(List<Tweet> Tweets) {
            Events.NewTweets.Found(Tweets, View);
        }

        public void ToggleUpdateVisibility() { 
            View.IsVisible = !View.IsVisible;
        }

        public void KeyDown(Key Key) {
            if (Key == Key.Enter) Search();
        }
    }
}