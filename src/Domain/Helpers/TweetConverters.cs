using System.Collections.Generic;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Model;

namespace Zunzun.Domain.Helpers {
    public static class TweetConverters {
        
        public static IEnumerable<Tweet> ToTweets(this string Response) {
            foreach (var Status in Response.AsStatuses())
                yield return Status.ToTweet();
        }
        
        public static Tweet ToTweet(this TwitterStatus Status) { 
            return ObjectFactory.NewTweet(Status);
        }
    }
}