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

        public TweetService TweetService { get; set; }

        public List<Tweet> GetConversation(Tweet tweet)
        {
            tweets = TweetService.Tweets;
            rootTweet = RootTweet(tweet);

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

        protected Tweet RootTweet(Tweet tweet)
        {
            var currentTweet = tweet;
            
            while (currentTweet.ReplyTo > 0)
            {
                var tempTweet = currentTweet;
                currentTweet = tweets.Where(x => x.Id == tempTweet.ReplyTo).First();
            }

            return currentTweet;
        } 

    }
}