using System;
using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using TweetMcQueen.Domain.Helpers;

namespace TweetMcQueen.Domain.Classes {
    
    public class TweetsServiceClass : TweetsService {
        
        public List<Tweet> Tweets { get {
            return Home.Request().ToTweets()
        ;}}

        public virtual IFluentTwitter Home { get {
            throw new NotImplementedException()
        ;}}
    }
}