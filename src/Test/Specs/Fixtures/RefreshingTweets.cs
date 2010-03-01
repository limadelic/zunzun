using System;
using System.Collections.Generic;
using System.Linq;
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
        Tweet OriginalFirstTweet;

        readonly Tweet Tweet = Actors.UniqueTweet;
        
        List<Tweet> TweetsShown { get { return HomePresenter.TweetCache; } }
        
        public RefreshingTweets() {
            HomeView = Create.TestObjectFor<HomeView>();

            HomePresenter = PresenterFactory.NewHomePresenter(HomeView);
            HomePresenter.Timer = new TestTimer();
        }

        protected override void SetUpSteps() {

            Given("the Refresh Cycle is {0} seconds", RefreshCycle => 
                App.Settings.DefaultRefreshCycle = Convert.ToInt32(RefreshCycle) * 1000
            );

            And("Home is shown", () => {
                HomePresenter.Load();
                OriginalFirstTweet = TweetsShown[0];
            }); 
            
            When("Status is updated", () => 
                HomePresenter.TweetService.UpdateStatus(Tweet)
            );
            
            And("{0} seconds have passed", WaitTime =>  
                Thread.Sleep(Convert.ToInt32(WaitTime)*1000)
            );
            
            Then("Home should contain the Tweet", () => 
                TweetsShown.ToList().ShouldContain(Tweet)
            );

            And("the Tweet should be shown above the older ones", () => {
                TweetsShown.ToList().ShouldContain(Tweet);
                TweetsShown.IndexOf(Tweet).ShouldBeLessThan(TweetsShown.IndexOf(OriginalFirstTweet));}
            );
        }
    }
}