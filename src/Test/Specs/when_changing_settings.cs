using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;

namespace Zunzun.Specs {

    public class when_changing_settings {
        
        [TestClass]
        public class a_SettingsPresenter : BehaviorOf<SettingsPresenter> {
            
            [TestMethod]
            public void should_offer_all_available_services_for_shortening_urls() {

                When.Load();
                Should.View.UrlShrinkers = Domain.Settings.UrlShrinkers;
            }
        }
    }
}