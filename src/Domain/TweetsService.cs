using System.Collections.Generic;

namespace TweetMcQueen.Domain {
    
    public interface TweetsService {
    
        List<Tweet> Tweets { get; }
    }
}