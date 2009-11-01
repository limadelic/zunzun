using System.Collections.Generic;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : HomeView {
    
        public List<Tweet> Tweets {
            get { return HomeLBX.ItemsSource as List<Tweet>; } 
            set { HomeLBX.ItemsSource = value; }
        }

        HomePresenter Presenter { get; set; }
    
        public Main() {
            InitializeComponent();
            Presenter = PresenterFactory.NewHomePresenter(this);
        }

        private void Close(object sender, System.Windows.RoutedEventArgs e) {
            Close();
        }

        private void Load(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Show();
        }

        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }
    }
}