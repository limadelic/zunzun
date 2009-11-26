using System.Windows;
using Zunzun.App.Events;

namespace Zunzun.App.Views.Xaml {

    public partial class Main {
        
        public Main() {
            InitializeComponent();
            RegisterEvents();
        }
        
        void RegisterEvents() {
            
            AddHandler(ShowUserHome.Event, new RoutedEventHandler(OnShowUserHome));
            AddHandler(FollowUser.Event, new RoutedEventHandler(OnFollowUser));
            
            AddHandler(Reply.Event, new RoutedEventHandler(Update.OnReply));
            AddHandler(Retweet.Event, new RoutedEventHandler(Update.OnRetweet));
            AddHandler(DirectMessage.Event, new RoutedEventHandler(Update.OnDirectMessage));
            
            ToggleUpdate.Click += Update.OnToggleVisibility;
        }

        void OnClose(object sender, RoutedEventArgs e) {
            Close();
        }

        void OnDragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnShowUserHome(object Sender, RoutedEventArgs e) {
            var UserHome = new UserHome();
            UserHome.OnShowUserHome(Sender, e);
            ContentPlaceholder.Child = UserHome;
        }

        void OnGoHome(object sender, RoutedEventArgs e) {
            ContentPlaceholder.Child = Home;
        }

        void OnFollowUser(object Sender, RoutedEventArgs E) {
            var UserName = (E as UserEvent.Args).UserName;
            var UserService = Domain.ObjectFactory.NewUserService;
            
            UserService.Follow(UserName);
        }
    }
}