using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : HomeView, StatusView {
    
        public ObservableCollection<Tweet> Tweets { get; set; }
        
        public bool IsUpdateVisible {
            get { return UpdateBOX.Visibility == Visibility.Visible; } 
            set { UpdateBOX.Visibility = value ? Visibility.Visible : Visibility.Collapsed; } 
        }

        public string UpdateText { 
            get { return Update.Text; }
            set { Update.Text = value; } 
        }

        HomePresenter HomePresenter { get; set; }
        StatusPresenter StatusPresenter { get; set;}
    
        public Main() {
            
            Tweets = new ObservableCollection<Tweet>();

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
        }

        private void OnToggleUpdate(object sender, RoutedEventArgs e)
        {
            StatusPresenter.ToggleUpdateVisibility();
        }
    }
}