using System.Collections.Generic;

namespace Zunzun.Domain {
    
    public interface TweetService {
    
        List<Tweet> Tweets { get; }
        void UpdateStatus(Tweet Tweet);
        void UpdateRefreshCycle(int RefreshCycle);
    }
}