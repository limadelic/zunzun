using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Converters;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class UserHome : UserHomeView {
    
        UserHomePresenter UserHomePresenter { get; set; }
    
        public ObservableCollection<Tweet> Tweets { get; set; }
        
        public bool AllowToFollow { set { Follow.IsVisibleIf(value); } }

        public bool AllowToUnfollow { set { Unfollow.IsVisibleIf(value); } }

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
            var UserName = (e as UserEvent.Args).UserName;
            UserHomePresenter.Show(UserName);
        }

        private void OnFollow(object Sender, RoutedEventArgs e) {
            FollowUser.Of(User.UserName, Sender);
        }
    }
}