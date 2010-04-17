using System;
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
                var list = new List<long> { rootTweet.Id };
                ConversationIdsHelper(rootTweet, list);
                return list;
            }
        }

        private void ConversationIdsHelper(Tweet currentTweet, ICollection<long> ids)
        {
            var children = Tweets.Where(tweet => tweet.ReplyTo == currentTweet.Id);

            if (children.Count() == 0) return;

            children.ForEach(tweet =>
            {
                ids.Add(tweet.Id);
                ConversationIdsHelper(tweet, ids);
            });
        }

        protected Tweet RootTweet(Tweet tweet)
        {
            var currentTweet = tweet;

            while (currentTweet.ReplyTo > 0)
            {
                var tempTweet = currentTweet;
                currentTweet = Tweets.Where(x => x.Id == tempTweet.ReplyTo).First();
            }

            return currentTweet;
        }
    }
}