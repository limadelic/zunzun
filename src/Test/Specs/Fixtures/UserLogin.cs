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
                Domain.Settings.UserName = UserName;
                Domain.Settings.Password = Password;
            });
            
            When("the program is launched", () => ZunzunPresenter.Load());
            
            Then("the user should be requested to login", () => 
                ZunzunView.Should().RequestLogin());
                
            Then("the user should not be requested to login", () => 
                ZunzunView.ShouldNot().RequestLogin());

            And("the credentials should be loaded", () => {
                Domain.Settings.UserName.ShouldBe(UserName);
                Domain.Settings.Password.ShouldBe(Password);
            });
        }
        
        string BackupUserName = "username";
        string BackupPassword = "password";
        
        public void BackupCredentials() {
            BackupUserName = Domain.Settings.UserName;
            BackupPassword = Domain.Settings.Password;
        }
        
        public void RestoreCredentials() {
            Domain.Settings.UserName = BackupUserName;
            Domain.Settings.Password = BackupPassword;
        }
    }
}