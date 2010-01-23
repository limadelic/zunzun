using Dimebrain.TweetSharp.Model;
using Zunzun.Domain.Classes;
using Zunzun.Domain.PhotoWebServices;

namespace Zunzun.Domain {

    public static class ObjectFactory {
        
        public static Tweet NewTweet(TwitterStatus Status) { return new TweetClass {
            Id = Status.Id,
            Content = Status.Text,
            Author = NewUser(Status.User),
            Picture = Status.User.ProfileImageUrl,
            Date = Status.CreatedDate.ToString(),
            Source = Status.Source,
            ReplyTo = Status.InReplyToStatusId
        };}

        public static Tweet NewTweet(string StatusText) { 
            return new TweetClass { Content = StatusText };
        }

        public static TweetService NewTweetService { get { return new TweetServiceClass(); } }

        public static UserService NewUserService { get { return new UserServiceClass(); } }
        public static UrlShrinker NewUrlShrinker { get { return new UrlShrinkerClass {
            WebRequest = Utils.ObjectFactory.NewWebRequest
        };}}

        public static PhotoWebService NewPhotoWebService { get { return new TwitPic(); } }
        public static PhotoWebService NewTwitPic { get { return new TwitPic(); } }

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