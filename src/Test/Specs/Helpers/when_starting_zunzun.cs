using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Model.Classes;

namespace Zunzun.Specs.Helpers {

    public class when_starting_zunzun {

        [TestClass]
        public class the_ZunzunPresenter : BehaviorOfZunzunPresenter {

            [TestMethod]
            public void should_not_request_credentials_if_present() {

                Given.UserAuthenticator.HasCredentials.Is(true);
                When.Load();
                Then.View.ShouldNot().RequestLogin();
            }

        }
        
        [TestClass]
        public class a_UserAuthenticator : BehaviorOf<UserAuthenticatorClass> {
        
            void GivenCredentials(string UserName, string Password) {
                Domain.Settings.UserName = UserName;
                Domain.Settings.Password = Password;
            }

            [TestMethod]
            public void should_have_credentials_if_username_and_password_are_present() {
            
                GivenCredentials("UserName", "Password");
                When.HasCredentials.ShouldBeTrue();
            }

            [TestMethod]
            public void should_not_have_credentials_if_missing_username() {
                
                GivenCredentials(null, "Password");
                When.HasCredentials.ShouldBeFalse();
            }
            
            [TestMethod]
            public void should_not_have_credentials_if_missing_password() {
                
                GivenCredentials("UserName", string.Empty);
                When.HasCredentials.ShouldBeFalse();
            }
        }
    }
}