using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class SearchPresenter {
    
        public SearchView View { get; set; }
        public TweetService TweetService { get; set; }

        public void Search() {
            View.Tweets = TweetService.TweetsContaining(View.SearchText);
        }
    }
}