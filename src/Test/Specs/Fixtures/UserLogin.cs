using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class UserLogin : Spec {
    
        const string UserName = "username";
        const string Password = "password";

        ZunzunView ZunzunView;
        ZunzunPresenter ZunzunPresenter;
        
        void Launch() {
            ZunzunView = Create.TestObjectFor<ZunzunView>();
            ZunzunPresenter = PresenterFactory.NewZunzunPresenter(ZunzunView);
            ZunzunPresenter.Load();
        }

        protected override void SetUpSteps() {
        
            Given("the are no credentials recorded", () =>
                Helpers.Given.Credentials(null, null));

            Given("the credentials have been recorded", () => 
                Helpers.Given.Credentials(UserName, Password));
            
            When("the program is launched", Launch);
            
            Then("the user should be requested to login", () => 
                ZunzunView.Should().RequestLogin());
                
            Then("the user should not be requested to login", () => 
                ZunzunView.ShouldNot().RequestLogin());
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