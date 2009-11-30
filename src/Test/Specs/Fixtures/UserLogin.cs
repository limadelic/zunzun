using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class UserLogin : Spec {
    
        const string UserName = "username";
        const string Password = "password";

        ZunzunView ZunzunView;
        ZunzunPresenter ZunzunPresenter;
        
        LoginView LoginView;
        LoginPresenter LoginPresenter;
        
        protected override void SetUpSteps() {
        
            Given("the are no credentials recorded", () =>
                Helpers.Given.Credentials(null, null));

            Given("the credentials have been recorded", () => 
                Helpers.Given.Credentials(UserName, Password));
                
            Given("the user is requested to login", () => {
                LoginView = Create.TestObjectFor<LoginView>();
                LoginPresenter = PresenterFactory.NewLoginPresenter(LoginView);
            });
            
            When("the program is launched", () => {
                ZunzunView = Create.TestObjectFor<ZunzunView>();
                ZunzunPresenter = PresenterFactory.NewZunzunPresenter(ZunzunView);
                ZunzunPresenter.Load();
            });
            
            When("invalid credentials are supplied", () => {
                LoginView.Given().UserName.Is(BackupUserName);
                LoginView.Given().Password.Is("invalid pass");
                LoginPresenter.Login();
            });
            
            When("the correct credentials are supplied", () => {
                LoginView.Given().UserName.Is(BackupUserName);
                LoginView.Given().Password.Is(BackupPassword);
                LoginPresenter.Login();
            });
            
            Then("the user should be requested to login", () => 
                ZunzunView.Should().RequestLogin());
                
            Then("the user should not be requested to login", () => 
                ZunzunView.ShouldNot().RequestLogin());

            Then("the credentials should be recorded", () => {
                Domain.Settings.UserName.ShouldBe(BackupUserName);
                Domain.Settings.Password.ShouldBe(BackupPassword);
            });

            Then("an error message should be displayed", () => 
                LoginView.Should().ShowError());
        }

        string BackupUserName;
        string BackupPassword;
        
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