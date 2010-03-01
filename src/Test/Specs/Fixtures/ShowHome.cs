using System.Linq;
using FluentSpec;
using Zunzun.Specs.Helpers;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.Specs.Fixtures {

    public class ShowHome : Spec {
    
        readonly HomePresenter HomePresenter;
        readonly HomeView HomeView;
        
        Tweet Tweet { get { return HomePresenter.TweetCache[0]; } }
        
        public ShowHome() {
            HomeView = Create.TestObjectFor<HomeView>();
            
            HomePresenter = PresenterFactory.NewHomePresenter(HomeView);
        }

        protected override void SetUpSteps() {

            When("Home is shown", () => HomePresenter.Show());
            
            Then("it should contain Tweets", () => HomePresenter.TweetCache.ToList().ShouldNotBeEmpty());

            When("a Tweet is displayed", () => HomePresenter.Show());
            
            And("should contain a Content", () => Tweet.Content.ShouldNotBeEmpty());
            
            And("an Author", () => Tweet.Author.ShouldNotBeNull());
            
            And("a Date", () => Tweet.Date.ShouldNotBeEmpty());
            
            And("a Source", () => Tweet.Source.ShouldNotBeEmpty());
            
            And("the Author's Picture", () => Tweet.Picture.ShouldNotBeEmpty());
        }
    }
}