using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Dimebrain.TweetSharp.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;
using FluentSpec;


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
                Given.Tweets.Are(Tweets);

                When.Show();

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_clear_before_displaying_Tweets() {
            
                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.TwoTweets);
                Given.Tweets.Are(Tweets);

                When.Show();

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_cache_the_Tweets() {
                Given.TweetService.Tweets.Are(Tweets);

                When.Show();

                The.Tweets.ShouldBe(Tweets);
            }

            [TestMethod]
            public void should_refresh_periodically() {
                When.SetupTimer();

                Should.Timer.Notify += The.CheckForNewTweets;
                Should.Timer.NotifyEvery(App.Settings.DefaultRefreshCycle);
            }

            [TestMethod]
            public void should_keep_the_Tweets_synced() {
                var LatestTweet = Tweets[0];
                var NewTweets = Actors.TwoTweets;

                Given.View.Tweets = new ObservableCollection<Tweet>();
                Given.Tweets.Is(Tweets);
                Given.TweetService.TweetsSince(LatestTweet.Id).Are(NewTweets);

                When.CheckForNewTweets();

                The.View.Tweets.ToList().ShouldBe(NewTweets);
            }

            [TestMethod]
            public void should_place_new_Tweets_above_older_ones() {
                var LatestTweet = Tweets[0];
                var NewTweet = Actors.UniqueTweet;

                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.TwoTweets);
                Given.Tweets.Are(Tweets);
                Given.TweetService.TweetsSince(LatestTweet.Id)
                    .Is(new List<Tweet> { NewTweet });

                When.CheckForNewTweets();

                The.View.Tweets[0].ShouldBe(NewTweet);
            }

            [TestMethod]
            public void should_do_nothing_when_in_Conversation_mode_and_there_are_no_new_Tweets() {
                var LatestTweet = Tweets[0];
                
                //we are displaying a conversation,
                Given.InConversationMode = true;
                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.ListOfTweetsWithReplyHierarchy);
                
                //and there are no new tweets
                Given.Tweets.Are(Tweets);
                Given.TweetService.TweetsSince(LatestTweet.Id)
                    .Is(new List<Tweet>());

                When.CheckForNewTweets();

                The.View.Tweets.ToList().ShouldBe(Actors.ListOfTweetsWithReplyHierarchy);
            }

            [TestMethod]
            public void should_replace_Conversation_with_Home_when_there_are_new_Tweets() {
                var LatestTweet = Tweets[0];
                var NewTweet = Actors.UniqueTweet;

                Given.InConversationMode = true;
                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.ListOfTweetsWithReplyHierarchy);
                Given.Tweets.Are(Tweets);
                Given.TweetService.TweetsSince(LatestTweet.Id)
                    .Is(new List<Tweet> { NewTweet });

                When.CheckForNewTweets();

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
        }

        [TestClass]
        public class the_Timer : BehaviorOf<TestTimer> {
        
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
                var Tweets = new List<Tweet>();
                
                Given.Tweets.Are(Tweets);
                Given.TweetsSinceSpec(42).WillReturn(FiveTweets);
                
                var ActualTweets = The.TweetsSince(42);
                
                ActualTweets.Count.ShouldBe(5);
                Tweets.ShouldContain(ActualTweets);
            }
        }
    }
}