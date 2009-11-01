using System.Collections.Generic;
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

            [TestMethod]
            public void should_display_the_Tweets() {
                var Tweets = new List<Tweet>();

                Given.TweetService.Tweets.Are(Tweets);
                When.Show();
                Then.View.Tweets.ShouldBe(Tweets);
            }
        }
        
        [TestClass]
        public class the_TweetsService : BehaviorOf<TweetServiceClass> {
        
            readonly ITwitterLeafNode Home = TestObjectFor<ITwitterLeafNode>();

            [TestInitialize]
            public void SetUp() { Given.Home.Is(Home); }

            [TestMethod]
            public void should_retrieve_the_Tweets() {
                
                Home.Given().Request().WillReturn(Actors.FiveTweets);
                The.Tweets.Count.ShouldBe(5);
            }
        }

    }
}