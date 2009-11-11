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
        
            Given[@"the User is ""UserName"""] = () =>
                UserName = Expected["UserName"];
            
            When["User Home is shown"] = () => 
                UserHomePresenter.Show(UserName);
            
            Then[@"should be named ""Name"""] = () => 
                UserHomeView.Name.ShouldBe(Expected["Name"]);
            
            And[@"should have joined on ""JoinedOn"""] = () => 
                UserHomeView.JoinedOn.ShouldBe(Expected["JoinedOn"]);
            
            And[@"the bio should be ""Bio"""] = () => 
                UserHomeView.Bio.ShouldBe(Expected["Bio"]);
            
            And["should be Following other users"] = () =>
                UserHomeView.Following.ShouldNotBe(0);
            
            And["should have Followers"] = () =>
                UserHomeView.Followers.ShouldNotBe(0);
            
            And["should have total of updates"] = () => 
                UserHomeView.UpdatesCount.ShouldNotBe(0);
            
            And[@"the website should be ""Website"""] = () =>
                UserHomeView.Website.ShouldBe(Expected["Website"]);
            
            And[@"the Twitter Home should be ""TwitterHome"""] = () =>
                UserHomeView.TwitterHome.ShouldBe(Expected["TwitterHome"]);
            
            And[@"the location should be ""Location"""] = () =>
                UserHomeView.Location.ShouldBe(Expected["Location"]);
            
            And[@"the time zone should be ""TimeZone"""] = () =>
                UserHomeView.TimeZone.ShouldBe(Expected["TimeZone"]);
        }
    }
}