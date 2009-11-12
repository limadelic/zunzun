using Dimebrain.TweetSharp.Model;
using Zunzun.Domain.Classes;

namespace Zunzun.Domain {

    public static class ObjectFactory {
        
        public static Tweet NewTweet(TwitterStatus Status) { return new TweetClass {
            Content = Status.Text,
            Author = Status.User.Name,
            Avatar = Status.User.ProfileImageUrl,
            Date = Status.CreatedDate.ToString(),
            Source = Status.Source,
            ScreenName = Status.User.ScreenName
        };}

        public static Tweet NewTweet(string StatusText) { 
            return new TweetClass { Content = StatusText };
        }

        public static TweetService NewTweetService { get { return new TweetServiceClass(); } }
    }
}