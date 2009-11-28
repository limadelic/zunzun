using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class UserLogin : Spec {
    
        const string UserName = "username";
        const string Password = "password";

        readonly ZunzunView ZunzunView;
        readonly ZunzunPresenter ZunzunPresenter;
        
        public UserLogin() {
            ZunzunView = Create.TestObjectFor<ZunzunView>();
            ZunzunPresenter = PresenterFactory.NewZunzunPresenter(ZunzunView);
        }

        protected override void SetUpSteps() {

            Given("the credentials have been recorded", () => {
                App.Settings.UserName = UserName;
                App.Settings.Password = Password;
            });
            
            When("the program is launched", () => ZunzunPresenter.Load());
            
            Then("the user login should not be requested", () => 
                ZunzunView.ShouldNot().RequestLogin());
                
            And("the credentials should be loaded", () => {
                Domain.Settings.UserName.ShouldBe(UserName);
                Domain.Settings.Password.ShouldBe(Password);
            });
        }
    }
}