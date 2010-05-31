using System.Collections.Generic;

namespace Zunzun.Domain {
    public interface Conversation {
        List<Tweet> Tweets { get; set; }
        List<Tweet> ConstructConversation(Tweet Tweet);
    }
}