using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain;

namespace Zunzun.App.Presenters
{
    public class ConversationPresenter
    {
        public List<Tweet> GetConversation(Tweet tweet)
        {
            return
                TweetService.Tweets.Where(postedTweet => postedTweet.Equals(tweet) || postedTweet.ReplyTo == tweet.Id).ToList(); 
        }

        public TweetService TweetService { get; set; }
    }
}