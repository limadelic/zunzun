using System;
using System.Collections.Generic;
using System.Windows.Navigation;
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

        void Navigate(object Sender, RequestNavigateEventArgs E) {
            System.Windows.MessageBox.Show("here");
        }

        private void Changed(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }
    }
}