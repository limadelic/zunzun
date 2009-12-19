using System;
using System.Collections.Generic;
using Zunzun.App.Controls;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {
    
    public partial class Settings : SettingsView {
    
        SettingsPresenter Presenter { get; set;}
    
        public Settings() {
            Presenter = PresenterFactory.NewSettingsPresenter(this);
            InitializeComponent();
        }

        public string UserName { get { return UserNameTXT.Text; } }
        
        public string Password { get { return PasswordTXT.Password; } }

        public List<string> UrlShrinkers { set { UrlShrinkersCBX.ItemsSource = value; } }
        
        public string UrlShrinker { 
            get { return UrlShrinkersCBX.SelectedItem.ToString(); }
            set { UrlShrinkersCBX.SelectedItem = value; } 
        }

        public void ShowError() {
            ErrorTXT.Show();
        }

        void HideError(object Sender, System.Windows.RoutedEventArgs Args) {
            ErrorTXT.Hide();
        }

        private void OnOK(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Login();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Load();
        }
    }
}
