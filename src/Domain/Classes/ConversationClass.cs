using System.Collections.Generic;
using System.Linq;
using Zunzun.Utils;

namespace Zunzun.Domain.Classes {
    public class ConversationClass : Conversation {
        Tweet rootTweet;
        public List<Tweet> Tweets { get; set; }

        public virtual List<Tweet> ConstructConversation(Tweet tweet)
        {
            rootTweet = RootTweet(tweet);
            return Tweets.Where(Tweet => ConversationIds.Contains(Tweet.Id)).ToList();
        }

        protected List<long> ConversationIds
        {
            get
            {
                var List = new List<long> { rootTweet.Id };
                ConversationIdsHelper(rootTweet, List);
                return List;
            }
        }

        private void ConversationIdsHelper(Tweet CurrentTweet, ICollection<long> Ids)
        {
            var Children = Tweets.Where(Tweet => Tweet.ReplyTo == CurrentTweet.Id);

            if (Children.Count() == 0) return;

            Children.ForEach(Tweet =>
            {
                Ids.Add(Tweet.Id);
                ConversationIdsHelper(Tweet, Ids);
            });
        }

        protected Tweet RootTweet(Tweet Tweet)
        {
            var CurrentTweet = Tweet;

            while (CurrentTweet.ReplyTo > 0)
            {
                var TempTweet = CurrentTweet;
                CurrentTweet = Tweets.Where(X => X.Id == TempTweet.ReplyTo).First();
            }

            return CurrentTweet;
        }
    }
}