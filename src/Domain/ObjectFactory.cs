using Dimebrain.TweetSharp.Model;
using Zunzun.Domain.Classes;

namespace Zunzun.Domain {

    public static class ObjectFactory {
        
        public static Tweet NewTweet(TwitterStatus Status) { return new TweetClass {
            Id = Status.Id,
            Content = Status.Text,
            Author = Status.User.Name,
            Picture = Status.User.ProfileImageUrl,
            Date = Status.CreatedDate.ToString(),
            Source = Status.Source,
            ScreenName = Status.User.ScreenName
        };}

        public static Tweet NewTweet(string StatusText) { 
            return new TweetClass { Content = StatusText };
        }

        public static TweetService NewTweetService { get { return new TweetServiceClass(); } }

        public static UserService NewUserService { get { return new UserServiceClass(); } }

        public static User NewUser(TwitterUser User) { return new UserClass {
            Name = User.Name,
            UserName = User.ScreenName,
            Picture = User.ProfileImageUrl,
            Bio = User.Description,
            JoinedOn = User.CreatedDate.ToString(),
            Following = User.FriendsCount,
            Followers = User.FollowersCount,
            UpdatesCount = User.StatusesCount,
            Website = User.Url,
            Location = User.Location,
            TimeZone = User.TimeZone
        };}
    }
}