using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {
    
    public class HomePresenter {
    
        public HomeView View { get; set; }
        public TweetService TweetService { get; set; }

        public void Show() {
            TweetService.Tweets.ForEach(View.Tweets.Add);
        }
    }
}