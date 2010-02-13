using System.Collections.Generic;
using Dimebrain.TweetSharp.Model;

namespace Zunzun.Domain.Helpers {

    public static class TweetConverters {
        
        public static List<Tweet> ToTweets(this IEnumerable<TwitterStatus> Statuses) {
            var Results = new List<Tweet>();

            // TODO: deal with errors
            if (Statuses == null) return Results;
            
            foreach (var Status in Statuses)
                Results.Add(ToTweet(Status));

            return Results;
        }

        public static List<Tweet> ToTweets(this TwitterSearchResult SearchResult) {
            var Results = new List<Tweet>();

            // TODO: deal with errors
            if (SearchResult == null) return Results;
            
            foreach (var Status in SearchResult.Statuses)
                Results.Add(ToTweet(Status));

            return Results;
        }

        static Tweet ToTweet(this TwitterStatus Status) { 
            return ObjectFactory.NewTweet(Status);
        }
    }
}