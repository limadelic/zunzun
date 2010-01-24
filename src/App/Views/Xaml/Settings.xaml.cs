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

        public string UserName {
            get { return UserNameTXT.Text; }
            set { UserNameTXT.Text = value; }
        }
        
        public string Password {
            get { return PasswordTXT.Password; }
            set { PasswordTXT.Password = value; }
        }

        public string UrlShrinker { 
            get { return UrlShrinkersCBX.SelectedItem.ToString(); }
            set { UrlShrinkersCBX.SelectedItem = value; } 
        }

        public List<string> UrlShrinkers {
            get { return UrlShrinkersCBX.ItemsSource as List<string>; }
            set { UrlShrinkersCBX.ItemsSource = value; }
        }
        
        public string PhotoService {
            get { return PhotoServicesCBX.SelectedItem.ToString(); }
            set { PhotoServicesCBX.SelectedItem = value; }
        }

        public List<string> PhotoServices {
            get { return PhotoServicesCBX.ItemsSource as List<string>; }
            set { PhotoServicesCBX.ItemsSource = value; }
        }

        public void ShowError() {
            ErrorTXT.Show();
        }

        void HideError(object Sender, System.Windows.RoutedEventArgs Args) {
            ErrorTXT.Hide();
        }

        private void OnOK(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Apply();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Load();
        }
    }
}
