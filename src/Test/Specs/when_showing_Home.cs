using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_showing_Home {
        
        [TestClass]
        public class the_Presenter : BehaviorOf<HomePresenter> {
        
            readonly List<Tweet> Tweets = Actors.TwoTweets;
        
            [TestMethod]
            public void should_display_the_Tweets() {
            
                Given.View.Tweets = new ObservableCollection<Tweet>();
                Given.TweetService.Tweets.Are(Tweets);

                When.Show();

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_keep_the_Tweets_updated() {
                var NewTweets = Actors.TwoTweets;

                Given.View.Tweets = new ObservableCollection<Tweet>(Tweets);

                When.NewTweetsAreAvailable(NewTweets);

                The.View.Tweets.ToList()
                    .ShouldBe(Tweets.Concat(NewTweets).ToList());
            }
        }
        
        [TestClass]
        public class the_TweetsService : BehaviorOf<TweetServiceClass> {
        
            readonly ITwitterLeafNode Home = TestObjectFor<ITwitterLeafNode>();

            [TestInitialize]
            public void SetUp() { Given.Home.Is(Home); }

            [TestMethod]
            public void should_retrieve_the_Tweets() {
                
                Home.Given().Request().WillReturn(Actors.FiveLiteralTweets);
                The.Tweets.Count.ShouldBe(5);
            }

            [TestMethod]
            public void should_notify_new_Tweets() {
                List<Tweet> ExpectedNewTweets = Actors.TwoTweets;
                List<Tweet> ActualNewTweets = null;

                Given.NewTweetsAreAvailable += Tweets => ActualNewTweets = Tweets;
                Given.NewTweets.Are(ExpectedNewTweets);

                When.CheckForNewTweets();
                
                ActualNewTweets.ShouldBe(ExpectedNewTweets);
            }
            
            [TestMethod]
            public void should_not_notify_if_there_are_no_new_Tweets() {
                
                Given.NewTweetsAreAvailable += Tweets => Assert.Fail();
                Given.NewTweets.Are(new List<Tweet>());

                this.ShouldNotFail(When.CheckForNewTweets);
            }

            [TestMethod]
            public void should_not_check_for_new_Tweets_if_no_one_is_expecting_them() {
                
                Given.NewTweets.WillThrow(new Exception());

                this.ShouldNotFail(When.CheckForNewTweets);
            }
        }
    }
}