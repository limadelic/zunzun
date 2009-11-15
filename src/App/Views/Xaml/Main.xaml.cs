using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : HomeView, StatusView, UserHomeView {
    
//        HomePresenter HomePresenter { get; set; }
        UserHomePresenter UserHomePresenter { get; set; }
        StatusPresenter StatusPresenter { get; set;}
    
        public Main() {
            
//            HomePresenter = PresenterFactory.NewHomePresenter(this);
            UserHomePresenter = PresenterFactory.NewUserHomePresenter(this);
            StatusPresenter = PresenterFactory.NewStatusPresenter(this);

            Tweets = new ObservableCollection<Tweet>();

            InitializeComponent();
            
            AddHandler(Events.ShowUserHome.ShowUserHomeEvent, new RoutedEventHandler(ShowUserHome), true);
        }

        public ObservableCollection<Tweet> Tweets { get; set; }
        public User User { get; set; }
        
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

        void Close(object sender, RoutedEventArgs e) {
            Close();
        }

        void Load(object sender, RoutedEventArgs e) {
//            HomePresenter.Load();
        }

        void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnSendUpdate(object sender, RoutedEventArgs e) {
        	StatusPresenter.Update();
        }

        void OnToggleUpdate(object sender, RoutedEventArgs e)
        {
            StatusPresenter.ToggleUpdateVisibility();
        }

        void Reply(object sender, RoutedEventArgs e)
        {
            var tweet = ((sender as Button).Tag as Tweet);
            StatusPresenter.ReplyTo( tweet );
        }

        void ShowUserHome(object Sender, RoutedEventArgs e) {
            var UserName = (e as Events.ShowUserHome.ShowUserHomeEventArgs).UserName;
            UserHomePresenter.Show(UserName);
        }

        void OnGoHome(object sender, RoutedEventArgs e) {
//        	HomePresenter.Show();
        }

    }
}