using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;
using ShouldAssertions=FluentSpec.ShouldAssertions;

namespace Zunzun.Specs {

    public class when_Searching_Tweets {
    
        [TestClass]
        public class a_SearchPresenter : BehaviorOf<SearchPresenter> {
        
            [TestMethod]
            public void should_find_and_show_tweets() {
                var Tweets = new List<Tweet>();
                
                Given.View.SearchText.Is("search text");
                Given.TweetService.TweetsContaining("search text").Are(Tweets);
                
                When.Search();
                
                ShouldAssertions.ShouldBe(The.View.Tweets, Tweets);
            }
        }
        
        [TestClass]
        public class a_TweetService : BehaviorOf<TweetServiceClass> {
            
            readonly ITwitterLeafNode FifteenTweetSpec = Actors.FifteenSearchTweetsTestSpec; 

            [TestMethod]
            public void should_retrieve_the_Tweets() {
            
                Given.TweetsContainingSpec("search text").Are(FifteenTweetSpec);
                The.TweetsContaining("search text").Count.ShouldBe(15);
            }
        }
    }
}