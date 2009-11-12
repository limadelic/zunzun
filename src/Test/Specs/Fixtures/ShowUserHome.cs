using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class ShowUserHome : Spec {

        string UserName;

        readonly UserHomePresenter UserHomePresenter;
        readonly UserHomeView UserHomeView;
        
        public ShowUserHome() {
            UserHomeView = Create.TestObjectFor<UserHomeView>();
            UserHomePresenter = PresenterFactory.NewUserHomePresenter(UserHomeView);
        }

        protected override void SetUpSteps() {
        
            Given("the User is {0}", UserName => 
                this.UserName = UserName);
            
            When("User Home is shown", () => 
                UserHomePresenter.Show(UserName));
            
            Then("should be named {0}", Name => 
                UserHomeView.Name.ShouldBe(Name));
            
            And("should have joined on {0}", JoinedOn => 
                UserHomeView.JoinedOn.ShouldBe(JoinedOn));
            
            And("the bio should be {0}", Bio => 
                UserHomeView.Bio.ShouldBe(Bio));
            
            And("should be Following other users", () =>
                UserHomeView.Following.ShouldNotBe(0));
            
            And("should have Followers", () =>
                UserHomeView.Followers.ShouldNotBe(0));
            
            And("should have total of updates", () => 
                UserHomeView.UpdatesCount.ShouldNotBe(0));
            
            And("the website should be {0}", Website =>
                UserHomeView.Website.ShouldBe(Website));
            
            And("the Twitter Home should be {0}", TwitterHome =>
                UserHomeView.TwitterHome.ShouldBe(TwitterHome));
            
            And("the location should be {0}", Location =>
                UserHomeView.Location.ShouldBe(Location));
            
            And("the time zone should be {0}", TimeZone =>
                UserHomeView.TimeZone.ShouldBe(TimeZone));
        }
    }
}