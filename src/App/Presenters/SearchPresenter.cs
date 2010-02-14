using System.Collections.Generic;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class SearchPresenter {
    
        public SearchView View { get; set; }
        public TweetService TweetService { get; set; }

        public void Search() {
            NotifyNewTweets(TweetService.TweetsContaining(View.SearchText));
        }

        public virtual void NotifyNewTweets(List<Tweet> Tweets) {
            Events.NewTweets.Found(Tweets, View);
        }
    }
}