using System.Windows;

namespace Zunzun.App.Views.Xaml {

    public partial class Main {
        
        public Main() {
            InitializeComponent();
            RegisterEvents();
        }
        
        void RegisterEvents() {
            
            AddHandler(Events.ShowUserHome.Event, new RoutedEventHandler(OnShowUserHome));
            
            AddHandler(Events.Reply.Event, new RoutedEventHandler(Update.OnReply));
            AddHandler(Events.Retweet.Event, new RoutedEventHandler(Update.OnRetweet));
            AddHandler(Events.DirectMessage.Event, new RoutedEventHandler(Update.OnDirectMessage));
            
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
    }
}