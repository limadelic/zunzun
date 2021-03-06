using System.Collections.Generic;
using System.Windows.Input;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    public class when_searching_Tweets {
    
        [TestClass]
        public class a_SearchPresenter : BehaviorOf<SearchPresenter> {
        
            [TestMethod]
            public void should_find_and_notify_tweets() {
                var Tweets = new List<Tweet>();
                
                Given.View.SearchText.Is("search text");
                Given.TweetService.TweetsContaining("search text").Are(Tweets);
                
                When.Search();
                
                Should.NotifyNewTweets(Tweets);
            }

            [TestMethod]
            public void should_toggle_visibility()
            {

                Given.View.IsVisible = true;

                When.ToggleUpdateVisibility();
                Then.View.IsVisible.ShouldBeFalse();

                When.ToggleUpdateVisibility();
                Then.View.IsVisible.ShouldBeTrue();
            }

            [TestMethod]
            public void should_trigger_search_on_enter() {

                When.KeyDown(Key.X);
                It.ShouldNot().Search();
                
                When.KeyDown(Key.Enter);
                It.Should().Search();
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