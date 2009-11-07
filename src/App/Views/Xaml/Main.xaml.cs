using System;
using System.Collections.Generic;
using System.Windows;
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

        public Visibility UpdateVisibility
        {
            get { 
                if(Update.Visibility != SendUpdate.Visibility) throw new Exception("The Update controls are out of sync");
                return Update.Visibility;
            }
            set { Update.Visibility = SendUpdate.Visibility = value; }
        }

        HomePresenter HomePresenter { get; set; }
        StatusPresenter StatusPresenter { get; set;}
    
        public Main() {
            InitializeComponent();
            HomePresenter = PresenterFactory.NewHomePresenter(this);
            StatusPresenter = PresenterFactory.NewStatusPresenter(this);
        }

        private void Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Load(object sender, RoutedEventArgs e) {
            HomePresenter.Show();
        }

        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        private void OnSendUpdate(object sender, RoutedEventArgs e) {
        	StatusPresenter.Update();
            HomePresenter.Show();
        }

        private void OnToggleUpdate(object sender, RoutedEventArgs e)
        {
            StatusPresenter.ToggleUpdateVisibility();
        }
    }
}