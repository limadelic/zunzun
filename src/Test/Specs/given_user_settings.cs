using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;
using UtilsSettings = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Specs {

    [TestClass]
    public class given_user_settings {
        
        const string Password = "secret";
        const string EncryptedPass = "@#$%%";

        [TestClass]
        public class when_applying_settings : BehaviorOf<SettingsPresenter> {
            
            [TestMethod]
            public void the_settings_should_be_saved() {

                When.Apply();
                Then.UserSettings.Should().Save();
            }
        }
        
        [TestClass]
        public class when_saving : BehaviorOf<UserSettingsClass> {
        
            [TestMethod]
            public void should_apply_settings() {

                Domain.Settings.UserName = "username";

                Domain.Settings.UrlShrinker = "url shrink";
                Domain.Settings.PhotoService = "photo up";
                
                When.Save();
                
                UtilsSettings.Default.UserName.ShouldBe("username");
                
                UtilsSettings.Default.UrlShrinker.ShouldBe("url shrink");
                UtilsSettings.Default.PhotoService.ShouldBe("photo up");
            }

            [TestMethod]
            public void should_encrypt_password() {

                Domain.Settings.Password = Password;
                Given.KeyMaker.Encrypt(Password).Is(EncryptedPass);
                
                When.Save();
                
                UtilsSettings.Default.Password.ShouldBe(EncryptedPass);
            }
            
            [TestMethod]
            public void should_store_settings() {
                
                When.Save();
                Should.Write();
            }
        }

        [TestClass]
        public class when_loading : BehaviorOf<UserSettingsClass> {
        
            [TestMethod]
            public void should_load_settings() {

                UtilsSettings.Default.UserName = "username";

                UtilsSettings.Default.UrlShrinker = "url shrink";
                UtilsSettings.Default.PhotoService = "photo up";
                
                When.Load();
                
                Domain.Settings.UserName.ShouldBe("username");
                
                Domain.Settings.UrlShrinker.ShouldBe("url shrink");
                Domain.Settings.PhotoService.ShouldBe("photo up");
            }

            [TestMethod]
            public void should_decrypt_password() {

                UtilsSettings.Default.Password = EncryptedPass;
                Given.KeyMaker.Decrypt(EncryptedPass).Is(Password);
                
                When.Load();
                
                Domain.Settings.Password.ShouldBe(Password);
            }
        }
    }
}