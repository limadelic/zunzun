using System.Collections.Generic;

namespace Zunzun.Domain {
    
    public interface TweetService {
    
        List<Tweet> Tweets { get; }
        List<Tweet> TweetsSince(long Id);
        List<Tweet> TweetsBy(User User);

        void UpdateStatus(Tweet Tweet);
        void SendReply(Tweet tweet);
    }
}