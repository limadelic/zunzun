using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;
using Zunzun.Utils;

namespace Zunzun.Specs.Fixtures {

    public class UserLogin : Spec {
    
        const string UserName = "username";

        ZunzunView ZunzunView;
        ZunzunPresenter ZunzunPresenter;
        
        LoginView LoginView;
        LoginPresenter LoginPresenter;

        KeyMaker KeyMaker = Utils.ObjectFactory.NewKeyMaker;
        
        protected override void SetUpSteps() {
        
            Given("the are no credentials recorded", () =>
                Helpers.Given.Credentials(null, null));

            Given("the credentials have been recorded", () => 
                Helpers.Given.Credentials(UserName, Actors.KinobotEncryptedPassword));
                
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
                LoginView.Given().UserName.Is(Actors.KinobotUserName);
                LoginView.Given().Password.Is("invalid pass");
                LoginPresenter.Login();
            });
            
            When("the correct credentials are supplied", () => {
                LoginView.Given().UserName.Is(Actors.KinobotUserName);
                LoginView.Given().Password.Is(Actors.KinobotPassword);
                LoginPresenter.Login();
            });
            
            Then("the user should be requested to login", () => 
                ZunzunView.Should().RequestLogin());
                
            Then("the user should not be requested to login", () => 
                ZunzunView.ShouldNot().RequestLogin());

            Then("the credentials should be recorded", () => {
                Utils.Properties.Settings.Default.UserName.ShouldBe(Actors.KinobotUserName);
                Utils.Properties.Settings.Default.Password.ShouldNotBeEmpty();
            });

            Then("an error message should be displayed", () => 
                LoginView.Should().ShowError());

            And("the password should be encrypted", () =>
                KeyMaker.Decrypt(Utils.Properties.Settings.Default.Password)
                    .ShouldBe(Actors.KinobotPassword));
        }

        #region backup/restore
        
        string BackupUserName;
        string BackupPassword;
        string BackupEncryptedPassword;

        public void BackupCredentials() {

            BackupUserName = Domain.Settings.UserName;
            BackupPassword = Domain.Settings.Password;
            BackupEncryptedPassword = Utils.Properties.Settings.Default.Password;
        }

        public void RestoreCredentials() {

            Domain.Settings.UserName = BackupUserName;
            Domain.Settings.Password = BackupPassword;

            Utils.Properties.Settings.Default.UserName = BackupUserName;
            Utils.Properties.Settings.Default.Password = BackupEncryptedPassword;
            Utils.Properties.Settings.Default.Save();
        }

        #endregion
    }
}