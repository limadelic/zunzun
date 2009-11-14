using System.Collections.Generic;

namespace Zunzun.Domain {
    
    public interface TweetService {
    
        List<Tweet> Tweets { get; }
        List<Tweet> TweetsSince(long Id);
        void UpdateStatus(Tweet Tweet);
    }
}