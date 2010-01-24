using System.Collections.Generic;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class ServicesConfig {
    
        readonly string UrlShrinkerBackup;
        readonly string PhotoServiceBackup;
        
        SettingsView SettingsView;
        SettingsPresenter SettingsPresenter;
        
        public ServicesConfig() {
            UrlShrinkerBackup = Domain.Settings.UrlShrinker;
            PhotoServiceBackup = Domain.Settings.PhotoService;
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
        
        public bool The_selected_Url_Shortener_should_be_saved() { return Verify.That(()=> {
            SettingsView.UrlShrinker = "tinyurl";
            SettingsPresenter.ApplyServicesSettings();
            Reload();
            SettingsView.UrlShrinker.ShouldBe("tinyurl");
        });}
        
        public List<string> The_Photo_Services_should_be() {
            return SettingsView.PhotoServices;
        }

        public bool The_selected_Photo_Service_should_be_saved() {  return Verify.That(()=> {
            SettingsView.PhotoService = "yfrog";
            SettingsPresenter.ApplyServicesSettings();
            Reload();
            SettingsView.PhotoService.ShouldBe("yfrog");
        });}
        
        public void TearDown() {
            Domain.Settings.UrlShrinker = UrlShrinkerBackup;
            Domain.Settings.PhotoService = PhotoServiceBackup;
        }
    }
}