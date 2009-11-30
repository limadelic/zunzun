using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Model.Classes;
using Zunzun.App.Presenters;

namespace Zunzun.Specs.Helpers {

    public class when_starting_zunzun {

        const string Password = "password";
        const string UserName = "username";
            
        [TestClass]
        public class the_ZunzunPresenter : BehaviorOfZunzunPresenter {

            [TestMethod]
            public void should_not_request_credentials_if_present() {

                Given.UserAuthenticator.HasCredentials.Is(true);
                When.Load();
                Then.View.ShouldNot().RequestLogin();
            }

            [TestMethod]
            public void should_request_credentials_if_missing() {

                Given.UserAuthenticator.HasCredentials.Is(false);
                When.Load();
                Then.View.Should().RequestLogin();
            }
        }
        
        [TestClass]
        public class a_UserAuthenticator : BehaviorOf<UserAuthenticatorClass> {
        
            [TestMethod]
            public void should_have_credentials_if_username_and_password_are_present() {
            
                Helpers.Given.Credentials("UserName", "Password");
                When.HasCredentials.ShouldBeTrue();
            }

            [TestMethod]
            public void should_not_have_credentials_if_missing_username() {
                
                Helpers.Given.Credentials(null, "Password");
                When.HasCredentials.ShouldBeFalse();
            }
            
            [TestMethod]
            public void should_not_have_credentials_if_missing_password() {
                
                Helpers.Given.Credentials("UserName", string.Empty);
                When.HasCredentials.ShouldBeFalse();
            }
            
            [TestMethod]
            public void should_apply_the_User_credentials() {
                
                When.Authenticate(UserName, Password);
                
                Domain.Settings.UserName.ShouldBe(UserName);
                Domain.Settings.Password.ShouldBe(Password);
            }
        }
        
        [TestClass]
        public class a_LoginPresenter : BehaviorOf<LoginPresenter> {

            [TestMethod]
            public void should_authenticate_credentials() {

                Given.View.UserName = UserName;
                Given.View.Password = Password;

                When.Login();
                
                Then.UserAuthenticator.Should()
                    .Authenticate(UserName, Password);
            }
            
            [TestMethod]
            public void should_close_upon_valid_credentials() {
                
                When.Login();
                Then.View.Should().Close();
            }
        }
    }
}