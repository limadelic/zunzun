using System;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Model.Classes;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs.Helpers {

    public class when_starting_zunzun {

        const string UserName = "username";
        const string Password = "password";
        const string EncryptedPassword = "!@##@$0054";
            
        [TestClass]
        public class the_ZunzunPresenter : BehaviorOfZunzunPresenter {

            [TestMethod]
            public void should_use_credentials_if_present() {

                Given.UserAuthenticator.HasCredentials.Is(true);
                When.Load();
                Then.View.ShouldNot().RequestLogin();
                Then.UserAuthenticator.Should().UseCredentials();
            }

            [TestMethod]
            public void should_request_credentials_if_missing() {

                Given.UserAuthenticator.HasCredentials.Is(false);
                When.Load();
                Then.View.Should().RequestLogin();
            }
        }
        
        [TestClass]
        public class a_UserAuthenticator_checking_the_credentials : BehaviorOf<UserAuthenticatorClass> {
        
            [TestMethod]
            public void should_have_credentials_if_username_and_password_are_present() {
            
                Helpers.Given.Credentials(UserName, EncryptedPassword);
                When.HasCredentials.ShouldBeTrue();
            }

            [TestMethod]
            public void should_not_have_credentials_if_missing_username() {
                
                Helpers.Given.Credentials(null, Password);
                When.HasCredentials.ShouldBeFalse();
            }
            
            [TestMethod]
            public void should_not_have_credentials_if_missing_password() {
                
                Helpers.Given.Credentials(UserName, string.Empty);
                When.HasCredentials.ShouldBeFalse();
            }
            
            [TestMethod]
            public void should_decrypt_the_password() {
                
                Helpers.Given.Credentials(UserName, EncryptedPassword);
                Given.KeyMaker.Decrypt(EncryptedPassword).WillReturn(Password);

                When.UseCredentials();
                Domain.Settings.Password.ShouldBe(Password);
            }
        }
        
        [TestClass]
        public class a_UserAuthenticator_authenticating : BehaviorOf<UserAuthenticatorClass> {

            [TestMethod]
            public void should_fail_if_credentials_are_not_valid() {

                Given.UserService.AreValid(UserName, Password).Is(false);
                this.ShouldFail(() => When.Authenticate(UserName, Password));
            }
            
            [TestMethod]
            public void should_apply_the_User_credentials() {
                
                Given.UserService.AreValid(UserName, Password).Is(true);
                
                When.Authenticate(UserName, Password);
                
                Domain.Settings.UserName.ShouldBe(UserName);
                Domain.Settings.Password.ShouldBe(Password);
            }
            
            [TestMethod]
            public void should_store_plain_username_and_encrypted_password() {

                Given.UserService.AreValid(UserName, Password).Is(true);
                Given.KeyMaker.Encrypt(Password).Is(EncryptedPassword);
                
                When.Authenticate(UserName, Password);
                
                Utils.Properties.Settings.Default.UserName
                    .ShouldBe(UserName);
                Utils.Properties.Settings.Default.Password
                    .ShouldBe(EncryptedPassword);
            }
            
            [TestMethod]
            public void should_save_the_credentials_to_storage_device() {
                
                Given.UserService.AreValid(UserName, Password).Is(true);
                When.Authenticate(UserName, Password);
                Should.SaveCredentials();
            }
        }
        
        [TestClass]
        public class a_LoginPresenter : BehaviorOf<LoginPresenter> {

            [TestMethod]
            public void should_authenticate_credentials() {

                Given.View.UserName.Is(UserName);
                Given.View.Password.Is(Password);

                When.Login();
                
                Then.UserAuthenticator.Should()
                    .Authenticate(UserName, Password);
            }
            
            [TestMethod]
            public void should_close_upon_valid_credentials() {
                
                When.Login();
                Then.View.Should().Close();
            }
            
            [TestMethod]
            public void should_show_error_upon_invalid_credentials() {
                
                Given.View.UserName.Is(UserName);
                Given.View.Password.Is(Password);
                
                Given.UserAuthenticator.Authenticate(UserName, Password); 
                    WillThrow(new Exception());
                
                When.Login();
                
                Then.View.Should().ShowError();
            }
        }
        
        [TestClass]
        public class a_UserService : BehaviorOf<UserServiceClass> {

            readonly ITwitterLeafNode ValidCredentialsSpec = Actors.CredentialsSpec;
            readonly ITwitterLeafNode InvalidCredentialsSpec = Actors.ErrorSpec;
            
            [TestMethod]
            public void should_validate_credentials() {
            
                Given.AreValidCredentialsSpec(UserName, Password).Is(ValidCredentialsSpec);
                When.AreValid(UserName, Password).ShouldBeTrue();
            }
            
            [TestMethod]
            public void should_detect_invalid_credentials() {
            
                Given.AreValidCredentialsSpec(UserName, Password).Is(InvalidCredentialsSpec);
                When.AreValid(UserName, Password).ShouldBeFalse();
            }
        }
    }
}