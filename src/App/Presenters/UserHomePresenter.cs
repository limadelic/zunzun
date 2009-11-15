using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UserHomePresenter {
    
        public UserHomeView View { get; set; }
        public UserService UserService { get; set; }
        public TweetService TweetService { get; set; }

        User User;

        public void Show(string UserName) {
            ShowUser(UserName);
            ShowUserTweets();
        }

        void ShowUserTweets() {
            View.Tweets.Clear();
            TweetService.TweetsBy(User).ForEach(View.Tweets.Add);
        }

        void ShowUser(string UserName) {
            User = UserService.FindByUserName(UserName);
            View.User = User;
        }
    }
}