using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class UserHome : UserHomeView {
    
        UserHomePresenter UserHomePresenter { get; set; }
    
        public ObservableCollection<Tweet> Tweets { get; set; }
        
        public User User {
            get { return DataContext as User; }
            set { DataContext = value; }
        }
        
        public UserHome() {
            Setup();
            InitializeComponent();
        }

        void Setup() {
            UserHomePresenter = PresenterFactory.NewUserHomePresenter(this);
            Tweets = new ObservableCollection<Tweet>();
        }
        
        public void OnShowUserHome(object Sender, RoutedEventArgs e) {
            var UserName = (e as Events.ShowUserHome.Args).UserName;
            UserHomePresenter.Show(UserName);
        }

    }
}