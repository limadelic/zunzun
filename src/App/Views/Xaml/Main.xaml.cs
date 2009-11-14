using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : HomeView, StatusView {
    
        HomePresenter HomePresenter { get; set; }
        StatusPresenter StatusPresenter { get; set;}
    
        public Main() {
            
            HomePresenter = PresenterFactory.NewHomePresenter(this);
            StatusPresenter = PresenterFactory.NewStatusPresenter(this);

            Tweets = new ObservableCollection<Tweet>();

            InitializeComponent();
        }

        public ObservableCollection<Tweet> Tweets { get; set; }
        
        public bool IsUpdateVisible {
            get { return UpdateBOX.Visibility == Visibility.Visible; } 
            set { UpdateBOX.Visibility = value ? Visibility.Visible : Visibility.Collapsed; } 
        }

        public void FocusOnUpdate()
        {
            Update.Focus();
        }

        public string UpdateText { 
            get { return Update.Text; }
            set { Update.Text = value; } 
        }

        private void Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Load(object sender, RoutedEventArgs e) {
            HomePresenter.Load();
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

        private void Reply(object sender, RoutedEventArgs e)
        {
            var tweet = ((sender as Button).Tag as Tweet);
            StatusPresenter.ReplyTo( tweet );
        }
    }
}