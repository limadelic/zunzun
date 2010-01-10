using System.Collections.Generic;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class ServicesConfig {
    
        readonly string UrlShrinkerBackup;
        
        SettingsView SettingsView;
        SettingsPresenter SettingsPresenter;
        
        public ServicesConfig() {
            UrlShrinkerBackup = Domain.Settings.UrlShrinker;
            Reload();
        }

        void Reload() {
            SettingsView = Create.TestObjectFor<SettingsView>();
            SettingsPresenter = PresenterFactory.NewSettingsPresenter(SettingsView);
            SettingsPresenter.Load();
        }

        public List<string> The_Url_Shorteners_should_be() {
            return SettingsView.UrlShrinkers;
        }
        
        public bool The_selected_Url_Shortener_should_be_saved() {
            SettingsView.UrlShrinker = "tinyurl";
            SettingsPresenter.ApplyServicesSettings();
            Reload();
            return SettingsView.UrlShrinker == "tinyurl";
        }
        
        public void TearDown() {
            Domain.Settings.UrlShrinker = UrlShrinkerBackup;
        }
    }
}