using System;
using System.Threading;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class RefreshingTweets : Spec {
        
        readonly HomePresenter HomePresenter;
        readonly HomeView HomeView;
        readonly TweetService TweetService;
        
        Tweet Tweet { get; set; }
        
        public RefreshingTweets() {
            HomeView = Create.TestObjectFor<HomeView>();
            HomePresenter = PresenterFactory.NewHomePresenter(HomeView);
            TweetService = HomePresenter.TweetService;
        }

        protected override void SetUpSteps() {

            Given["Home is shown"] = () => {
                HomePresenter.Show(); 
            };
            
            And[@"the Refresh Cycle is ""Refresh Cycle"" seconds"] = () => {
                var RefreshCycle = Convert.ToInt32(Expected["Refresh Cycle"]);
                
                TweetService.UpdateRefreshCycle(RefreshCycle); 
            };
            
            When[@"Status is updated to ""Tweet Content"""] = () => {
            
                Tweet = Domain.ObjectFactory.NewTweet(Expected["Tweet Content"]);
                
                HomePresenter.TweetService.UpdateStatus(Tweet);
            };
            
            And[@"""Wait Time"" seconds have passed"] = () => {
                var WaitTime = Convert.ToInt32(Expected["Wait Time"]);
                
                Thread.Sleep(WaitTime);
            };
            
            Then["Home should contain the Tweet"] = () => {
                
                TweetService.Tweets.ShouldContain(Tweet);
            };
        }
    }
}