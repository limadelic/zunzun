using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.Specs.Fixtures {

    public class ShowUserHome : Spec {

        string UserName;

        readonly UserHomePresenter UserHomePresenter;
        readonly UserHomeView UserHomeView;
        
        User User { get { return UserHomeView.User; }}
        
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
                User.Name.ShouldBe(Name));
            
            And("should have joined on {0}", JoinedOn => 
                User.JoinedOn.ShouldBe(JoinedOn));
            
            And("the bio should be {0}", Bio => 
                User.Bio.ShouldBe(Bio));
            
            And("should be Following other users", () =>
                User.Following.ShouldNotBe(0));
            
            And("should have Followers", () =>
                User.Followers.ShouldNotBe(0));
            
            And("should have total of updates", () => 
                User.UpdatesCount.ShouldNotBe(0));
            
            And("the website should be {0}", Website =>
                User.Website.ShouldBe(Website));
            
            And("the Twitter Home should be {0}", TwitterHome =>
                User.TwitterHome.ShouldBe(TwitterHome));
            
            And("the location should be {0}", Location =>
                User.Location.ShouldBe(Location));
            
            And("the time zone should be {0}", TimeZone =>
                User.TimeZone.ShouldBe(TimeZone));
        }
    }
}