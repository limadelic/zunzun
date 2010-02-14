using System.Collections.Generic;
using System.Linq;
using Dimebrain.TweetSharp.Model;

namespace Zunzun.Domain.Helpers {

    public static class TweetConverters {
        
        public static List<Tweet> ToTweets(this IEnumerable<TwitterStatus> Statuses) { 
            // TODO: deal with errors
            if (Statuses == null) return new List<Tweet>();
            
            return TweetsFrom(Statuses);
        }

        public static List<Tweet> ToTweets(this TwitterSearchResult SearchResult) { 
            // TODO: deal with errors
            if (SearchResult == null) return new List<Tweet>();
            
            return TweetsFrom(CastToStatuses(SearchResult.Statuses));
        }
        
        // Needed to let us handle both args to toTweets with the same function. Note that 
        // we traverse the list an extra time when this is called.
        static IEnumerable<TwitterStatus> CastToStatuses(IEnumerable<TwitterSearchStatus> SearchStatuses) { return
            from Status in SearchStatuses select (TwitterStatus)Status
        ;}

        static List<Tweet> TweetsFrom(IEnumerable<TwitterStatus> Statuses) { return 
            (from Status in Statuses select TweetFrom(Status)).ToList()
        ;}

        static Tweet TweetFrom(this TwitterStatus Status) { return 
            ObjectFactory.NewTweet(Status)
        ;}
    }
}