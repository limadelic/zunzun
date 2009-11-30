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
            
            // needs to go inside the Update control
            AddHandler(Reply.Event, new RoutedEventHandler(Update.OnReply));
            AddHandler(Retweet.Event, new RoutedEventHandler(Update.OnRetweet));
            AddHandler(DirectMessage.Event, new RoutedEventHandler(Update.OnDirectMessage));
            
            ToggleUpdate.Click += Update.OnToggleVisibility;
        }

        void OnClose(object Sender, RoutedEventArgs Args) {
            Close();
        }

        void OnDragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnGoHome(object Sender, RoutedEventArgs Args) { Show(Home); }

        public void Show(UIElement ContentControl) {
            ContentPlaceholder.Child = ContentControl;
        }

        public void RequestLogin() {
            var Login = new Login { Owner = this };
            Login.ShowDialog();
        }

        void Load(object Sender, RoutedEventArgs Args) {
            Presenter.Load();
        }
    }
}