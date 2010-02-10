using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;
using Zunzun.Utils;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Specs.Fixtures {

    public class UserLogin {
    
        ZunzunView ZunzunView;
        ZunzunPresenter ZunzunPresenter;
        
        SettingsView SettingsView;
        SettingsPresenter SettingsPresenter;

        readonly KeyMaker KeyMaker = Utils.ObjectFactory.NewKeyMaker;

        void LoadZunzun() {

            ZunzunView = Create.TestObjectFor<ZunzunView>();
            ZunzunPresenter = PresenterFactory.NewZunzunPresenter(ZunzunView);

            ZunzunPresenter.Load();
        }

        void SetUpSetings() {
            SettingsView = Create.TestObjectFor<SettingsView>();
            SettingsPresenter = PresenterFactory.NewSettingsPresenter(SettingsView);
        }

        public bool The_user_credentials_should_be_requested_on_the_first_run_of_zunzun() { return Verify.That(() => {
            UtilsSettings.Default.UserName = null;

            LoadZunzun();

            ZunzunView.Should().RequestLogin();            
        });}

        public bool The_credentials_should_be_stored_with_encrypted_password() { return Verify.That(() => {
            UtilsSettings.Default.UserName = null;
            UtilsSettings.Default.Password = null;

            SetUpSetings();

            SettingsView.Given().UserName.Is(Actors.KinobotUserName);
            SettingsView.Given().Password.Is(Actors.KinobotPassword);
            
            SettingsPresenter.Apply();
            
            UtilsSettings.Default.UserName.ShouldBe(Actors.KinobotUserName);
            UtilsSettings.Default.Password.ShouldNotBeEmpty();
            KeyMaker.Decrypt(Utils.Properties.Settings.Default.Password)
                .ShouldBe(Actors.KinobotPassword);
        });}

        public bool Once_recorded_the_credentials_should_not_be_requested() { return Verify.That(() => {
            LoadZunzun();
            ZunzunView.ShouldNot().RequestLogin();
        });}
        
        public bool An_error_message_should_be_shown_if_the_credentials_are_invalid() { return Verify.That(() => {

            SetUpSetings();

            SettingsView.Given().UserName.Is(Actors.KinobotUserName);
            SettingsView.Given().Password.Is("invalid pass");

            SettingsPresenter.Login();

            SettingsView.Should().ShowError();
        });}
    }
}