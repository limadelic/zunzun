using System.Collections.Generic;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : HomeView, StatusView {
    
        public List<Tweet> Tweets {
            get { return HomeLBX.ItemsSource as List<Tweet>; } 
            set { HomeLBX.ItemsSource = value; }
        }

        public string UpdateText { 
            get { return Update.Text; }
            set { Update.Text = value; } 
        }
            
        HomePresenter HomePresenter { get; set; }
        StatusPresenter StatusPresenter { get; set;}
    
        public Main() {
            InitializeComponent();
            HomePresenter = PresenterFactory.NewHomePresenter(this);
            StatusPresenter = PresenterFactory.NewStatusPresenter(this);
        }

        private void Close(object sender, System.Windows.RoutedEventArgs e) {
            Close();
        }

        private void Load(object sender, System.Windows.RoutedEventArgs e) {
            HomePresenter.Show();
        }

        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        private void OnSendUpdate(object sender, System.Windows.RoutedEventArgs e) {
        	StatusPresenter.Update();
        }
    }
}