using System.Collections.Generic;

namespace Zunzun.Domain {
    
    public interface TweetService {
    
        List<Tweet> Tweets { get; }
        List<Tweet> NewTweets { get; }
        
        void UpdateStatus(Tweet Tweet);
    }
}