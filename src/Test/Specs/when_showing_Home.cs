using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
            public void should_be_ready_to_display_and_refresh_Tweets() {
            
                When.Load();
                
                Should.Show();
                Should.SetupTimer();
            }
            
            [TestMethod]
            public void should_display_the_Tweets() {
            
                Given.View.HomeTweets = new ObservableCollection<Tweet>();
                Given.TweetService.Tweets.Are(Tweets);

                When.Show();

                The.View.HomeTweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_clear_before_displaying_Tweets() {
            
                Given.View.HomeTweets = new ObservableCollection<Tweet>(Actors.TwoTweets);
                Given.TweetService.Tweets.Are(Tweets);

                When.Show();

                The.View.HomeTweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_refresh_periodically() {
                
                When.SetupTimer();

                Should.Timer.Notify += The.CheckForNewTweets;   
                Should.Timer.NotifyEvery(App.Settings.DefaultRefreshCycle);
            }
            
            [TestMethod]
            public void should_keep_the_Tweets_sync() {
                var CurrentTweets = new ObservableCollection<Tweet>(Tweets);
                var LatestTweet = CurrentTweets[0];
                var NewTweets = Actors.TwoTweets;

                Given.View.HomeTweets = CurrentTweets;
                Given.TweetService.TweetsSince(LatestTweet.Id).Are(NewTweets);

                When.CheckForNewTweets();

                The.View.HomeTweets.ToList()
                    .ShouldBe(NewTweets.Concat(Tweets).ToList());
            }

            [TestMethod]
            public void should_place_new_Tweets_above_older_ones() {
                var CurrentTweets = new ObservableCollection<Tweet>(Tweets);
                var LatestTweet = CurrentTweets[0];
                var NewTweet = Actors.UniqueTweet;

                Given.View.HomeTweets = new ObservableCollection<Tweet>(Tweets);
                Given.TweetService.TweetsSince(LatestTweet.Id).Is(new List<Tweet> {NewTweet});
                
                When.CheckForNewTweets();
                
                The.View.HomeTweets[0].ShouldBe(NewTweet);
            }
        }
        
        [TestClass]
        public class the_Timer : BehaviorOf<TestTimer>{
            
            [Ignore] // timer works only with the UI loaded
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
        public class the_TweetService : BehaviorOf<TweetServiceClass> {
        
            readonly ITwitterLeafNode FiveTweets = Actors.FiveTweetsTestSpec; 

            [TestMethod]
            public void should_retrieve_the_Tweets() {
                
                Given.HomeSpec.Is(FiveTweets);
                The.Tweets.Count.ShouldBe(5);
            }
            
            [TestMethod]
            public void should_retrieve_Tweets_since_latest_id() {
                
                Given.TweetsSinceSpec(42).WillReturn(FiveTweets);
                The.TweetsSince(42).Count.ShouldBe(5);
            }
        }
    }
}