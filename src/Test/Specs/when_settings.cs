using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;

namespace Zunzun.Specs {

    public class when_settings {
        
        [TestClass]
        public class are_shown : BehaviorOf<SettingsPresenter> {
        
            [TestInitialize]
            public void SetUp() { When.Load(); }

            [TestMethod]
            public void should_offer_all_available_services_for_shortening_urls() {

                Should.View.UrlShrinkers = Domain.Settings.UrlShrinkers;
            }
            
            [TestMethod]
            public void should_select_current_service_for_shortening_urls() {

                The.View.UrlShrinker.ShouldBe(
                    Domain.Settings.UrlShrinker);
            }
            
            [TestMethod]
            public void should_load_current_credentials() {
                
                The.View.UserName.ShouldBe(
                    Domain.Settings.UserName);
                The.View.Password.ShouldBe(
                    Domain.Settings.Password);
            }
        }
        
        [TestClass]
        public class are_applied : BehaviorOf<SettingsPresenter> {
        
            const string UrlShrinker = "Url Shrinker";
            
            [TestMethod]
            public void should_be_saved() {
                
                When.Apply();
                Should.Save();
            }

            [TestMethod]
            public void should_set_the_UrlShrinker() {

                Given.View.UrlShrinker = UrlShrinker;

                When.ApplyServicesSettings();
                
                Domain.Settings.UrlShrinker.ShouldBe(UrlShrinker);
                Utils.Properties.Settings.Default.UrlShrinker
                    .ShouldBe(UrlShrinker);
            }
        }
    }
}