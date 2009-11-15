using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UserHomePresenter {
    
        public UserHomeView View { get; set; }
        public UserService UserService { get; set; }
        public TweetService TweetService { get; set; }

        public void Show(string UserName) {
            var User = UserService.FindByUserName(UserName);
            View.User = User;

            View.Tweets.Clear();
            TweetService.TweetsBy(User).ForEach(View.Tweets.Add);
        }
    }
}