using System.Linq;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UserHomePresenter {
    
        public UserHomeView View { get; set; }
        public UserService UserService { get; set; }
        public TweetService TweetService { get; set; }

        User User;

        public void Show(string UserName) {
            
            User = UserService.FindByUserName(UserName);
            
            ShowUser();
            ShowActions();
            ShowTweets();
        }

        void ShowTweets() {
            View.Tweets.Clear();
            TweetService.TweetsBy(User).ForEach(View.Tweets.Add);
        }

        void ShowUser() { View.User = User; }

        void ShowActions() {
            var isFollowing = IsFollowing;
            View.AllowToFollow = !isFollowing;
            View.AllowToUnfollow = isFollowing;
        }

        bool IsFollowing { get { return 
            UserService.Following.Any(FollowedUser =>
                FollowedUser.UserName.Equals(User.UserName))
        ;}}
    }
}