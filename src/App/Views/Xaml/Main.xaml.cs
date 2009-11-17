using System.Windows;
using Zunzun.App.Controls;

namespace Zunzun.App.Views.Xaml {

    public partial class Main {
    
        public Main() {
            InitializeComponent();
            RegisterEvents();
        }

        void RegisterEvents() {
            
            AddHandler(Events.ShowUserHome.Event, new RoutedEventHandler(OnShowUserHome));
            AddHandler(Events.ShowUserHome.Event, new RoutedEventHandler(UserHome.OnShowUserHome));
            
            AddHandler(Events.Reply.Event, new RoutedEventHandler(Update.OnReply));
            
            ToggleUpdate.Click += Update.OnToggleVisibility;
        }

        void OnClose(object sender, RoutedEventArgs e) {
            Close();
        }

        void OnDragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnShowUserHome(object Sender, RoutedEventArgs e) {
            UserHome.Show();
            Home.Hide();
        }

        void OnGoHome(object sender, RoutedEventArgs e) {
            Home.Show();
            UserHome.Hide();
        }
    }
}