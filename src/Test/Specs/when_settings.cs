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
        }
    }
}