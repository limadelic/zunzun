using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Model.Classes;
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
            public void should_be_ready_to_display_and_refresh_Tweets() {
            
                When.Load();
                
                Should.Show();
                Should.SetupTimer();
            }
            
            [TestMethod]
            public void should_display_the_Tweets() {
            
                Given.View.Tweets = new ObservableCollection<Tweet>();
                Given.TweetService.Tweets.Are(Tweets);

                When.Show();

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_refresh_periodically() {
                
                When.SetupTimer();

                Should.Timer.Notify += The.CheckForNewTweets;   
                Should.Timer.NotifyEvery(App.Settings.DefaultRefreshCycle);
            }
            
            [TestMethod]
            public void should_keep_the_Tweets_sync() {
                var NewTweets = Actors.TwoTweets;

                Given.View.Tweets = new ObservableCollection<Tweet>(Tweets);
                Given.TweetService.NewTweets.Are(NewTweets);

                When.CheckForNewTweets();

                The.View.Tweets.ToList()
                    .ShouldBe(NewTweets.Concat(Tweets).ToList());
            }

            [TestMethod]
            public void should_place_new_Tweets_above_older_ones() {
                var NewTweet = Actors.UniqueTweet;

                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.TwoTweets);
                Given.TweetService.NewTweets.Are(new List<Tweet> {NewTweet});
                
                When.CheckForNewTweets();
                
                The.View.Tweets[0].ShouldBe(NewTweet);
            }
        }
        
        [TestClass]
        public class the_Timer : BehaviorOf<TimerClass>{
            
            [TestMethod]
            public void should_tick() {
            
                var TickReceived = false;

                Given.Notify += () => TickReceived = true;
                
                When.NotifyEvery(Actors.OneMillisecond);

                Thread.Sleep(Actors.OneHundredMilliseconds);
                
                TickReceived.ShouldBeTrue();
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
        }
    }
}