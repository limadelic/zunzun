using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain;
using Zunzun.Domain.Helpers;

namespace Zunzun.App.Presenters
{
    public class ConversationPresenter
    {
        private Tweet rootTweet;
        private List<Tweet> tweets;

        public List<Tweet> GetConversation(Tweet tweet)
        {
            tweets = TweetService.Tweets;
            rootTweet = tweet;

            return tweets.Where(postedTweet => ConversationIds.Contains(postedTweet.Id)).ToList(); 
        }

        protected List<long> ConversationIds { get {
            var list = new List<long> {rootTweet.Id};
            ConversationIdsHelper(rootTweet, list);
            return list;
        } }

        private void ConversationIdsHelper(Tweet currentTweet, ICollection<long> ids)
        {
            var children = tweets.Where(tweet => tweet.ReplyTo == currentTweet.Id);

            if(children.Count() == 0)
                return;

            children.ForEach(tweet =>
            {
                ConversationIdsHelper(tweet, ids);
                ids.Add(tweet.Id);
            });
        }

//        protected long RootId { get
//        {
//            var currentTweet = rootTweet;
//            while(currentTweet.ReplyTo > 0)
//                currentTweet = TweetService.TweetById()
//        } }

        public TweetService TweetService { get; set; }
    }
}