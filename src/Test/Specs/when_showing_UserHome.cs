using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.App.Views.Xaml;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_showing_UserHome {
        
        static readonly User Zunzun = Actors.Zunzun;
        
        [TestClass]
        public class the_ZunzunPresenter : BehaviorOfZunzunPresenter {
            
            [TestMethod]
            public void should_handle_the_show_event() {
                
                var OnShowUserHome = GivenHandlerFor(Expected.OnShowUserHome);
                When.RegisterEvents();
                Then.View.Should().AddHandler(ShowUserHome.Event, OnShowUserHome);
            }
        }
        
        [TestClass]
        public class the_ZunzunPresenter_on_show : BehaviorOfZunzunPresenter {
            
            readonly object Sender = new object();
            readonly UserHome UserHome = new UserHome();
            readonly RoutedEventArgs Args = new RoutedEventArgs();
            Delegate OnUserChanged;

            [TestInitialize]
            public void SetUp() {
                OnUserChanged = GivenHandlerFor(UserHome.OnUserChanged);
                Given.NewUserHome.Is(UserHome);
                
                When.OnShowUserHome(Sender, Args);
            }

            [TestMethod]
            public void should_delegate_on_UserHome_control() {
                Should.Show(UserHome, Sender, Args);
            }

            [TestMethod]
            public void should_notify_UserHome_about_UserChanges() {
                Then.View.Should().AddHandler(UserChanged.Event, OnUserChanged);
            }
            
            [TestMethod]
            public void should_display_the_UserHome() {
                Then.View.Should().Show(UserHome);
            }
        }
        
        [TestClass]
        public class the_Presenter : BehaviorOf<UserHomePresenter> {
        
            readonly List<Tweet> Tweets = Actors.TwoTweets;
            
            [TestInitialize]
            public void SetUp() {
                Given.UserService.FindByUserName(Zunzun.UserName).WillReturn(Zunzun);
            }

            [TestMethod]
            public void should_display_the_User_details() {
                
                When.Show(Zunzun.UserName);
                The.View.User.ShouldBe(Zunzun);
            }

            [TestMethod]
            public void should_display_the_Tweets() {
            
                Given.View.Tweets = new ObservableCollection<Tweet>();
                Given.TweetService.TweetsBy(Zunzun).Are(Tweets);

                When.Show(Zunzun.UserName);

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
            
            [TestMethod]
            public void should_clear_before_displaying_Tweets() {
                
                Given.View.Tweets = new ObservableCollection<Tweet>(Actors.TwoTweets);
                Given.TweetService.TweetsBy(Zunzun).Are(Tweets);

                When.Show(Zunzun.UserName);

                The.View.Tweets.ToList().ShouldBe(Tweets);
            }
        }
        
        [TestClass]
        public class the_UserService : BehaviorOf<UserServiceClass> {
            
            [TestMethod]
            public void should_retrieve_a_user_by_UserName() {
                const string UserName = Actors.ZunzunUserName;
                var Spec = Actors.ZunzunTestSpec;
                
                Given.UserByUserNameSpec(UserName).WillReturn(Spec);
                
                var UserFound = When.FindByUserName(UserName);
                
                UserFound.UserName.ShouldBe(UserName);
            }
        }

        [TestClass]
        public class the_TweetService : BehaviorOf<TweetServiceClass> {
        
            readonly ITwitterLeafNode Spec = Actors.FiveTweetsTestSpec; 

            [TestMethod]
            public void should_retrieve_Tweets_since_latest_id() {
                
                Given.TweetsByUserNameSpec(Zunzun.UserName).Is(Spec);
                The.TweetsBy(Zunzun).Count.ShouldBe(5);
            }
        }
    }
}