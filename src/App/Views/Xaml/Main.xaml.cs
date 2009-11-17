using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : UserHomeView {
    
        UserHomePresenter UserHomePresenter { get; set; }
    
        public Main() {
            Setup();
            InitializeComponent();
            RegisterEvents();
        }

        void Setup() {
            UserHomePresenter = PresenterFactory.NewUserHomePresenter(this);
            Tweets = new ObservableCollection<Tweet>();
        }
        
        void RegisterEvents() {
            AddHandler(Events.ShowUserHome.Event, new RoutedEventHandler(OnShowUserHome));
            AddHandler(Events.Reply.Event, new RoutedEventHandler(Update.OnReply));
            ToggleUpdate.Click += Update.OnToggleVisibility;
        }

        public ObservableCollection<Tweet> Tweets { get; set; }
        public User User { get; set; }
        
        void OnClose(object sender, RoutedEventArgs e) {
            Close();
        }

        void OnDragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnShowUserHome(object Sender, RoutedEventArgs e) {
            var UserName = (e as Events.ShowUserHome.Args).UserName;
            UserHomePresenter.Show(UserName);
        }

        void OnGoHome(object sender, RoutedEventArgs e) {
        }
    }
}