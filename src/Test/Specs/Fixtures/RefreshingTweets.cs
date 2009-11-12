using System;
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
        readonly TweetService TweetService;

        Tweet Tweet { get; set; }
        
        public RefreshingTweets() {
            HomeView = Create.TestObjectFor<HomeView>();
            HomePresenter = PresenterFactory.NewHomePresenter(HomeView);
            TweetService = HomePresenter.TweetService;
        }

        protected override void SetUpSteps() {

            Given("Home is shown", () => HomePresenter.Show()); 
            
            And("the Refresh Cycle is {0} seconds", RefreshCycle => 
                TweetService.UpdateRefreshCycle(Convert.ToInt32(RefreshCycle)) 
            );

            When("Status is updated", () => {
            
                Tweet = Actors.UniqueTweet;

                HomePresenter.TweetService.UpdateStatus(Tweet);
            });
            
            And("{0} seconds have passed", WaitTime => 
                Thread.Sleep(Convert.ToInt32(WaitTime))
            );
            
            Then("Home should contain the Tweet", () => 
                HomeView.Tweets.ToList().ShouldContain(Tweet)
            );
        }
    }
}