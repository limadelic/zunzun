using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : ZunzunView {
    
        readonly ZunzunPresenter Presenter; 
        
        public Main() {
            Presenter = PresenterFactory.NewZunzunPresenter(this);
            InitializeComponent();
            RegisterEvents();
        }
        
        void RegisterEvents() {
            
            AddHandler(ShowUserHome.Event, new RoutedEventHandler(OnShowUserHome));
            AddHandler(FollowUser.Event, new RoutedEventHandler(OnFollowUser));
            AddHandler(UnfollowUser.Event, new RoutedEventHandler(OnUnfollowUser));
            
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
            AddHandler(UserChanged.Event, new RoutedEventHandler(UserHome.OnUserChanged));
            ContentPlaceholder.Child = UserHome;
        }

        void OnGoHome(object sender, RoutedEventArgs e) {
            ContentPlaceholder.Child = Home;
        }

        void OnFollowUser(object Sender, RoutedEventArgs E) {
            var UserName = (E as UserEvent.Args).UserName;
            Presenter.Follow(UserName);
        }

        void OnUnfollowUser(object Sender, RoutedEventArgs E) { 
            var UserName = (E as UserEvent.Args).UserName;
            Presenter.Unfollow(UserName);
        }
    }
}