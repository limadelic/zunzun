using System.Windows;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {

    public partial class Update : StatusView {
    
        StatusPresenter StatusPresenter { get; set; }

        public Update() {
            StatusPresenter = PresenterFactory.NewStatusPresenter(this);
            InitializeComponent();
        }

        public new bool IsVisible { 
            get { return base.IsVisible; } 
            set { Visibility = value ? Visibility.Visible : Visibility.Collapsed; } 
        }

        public void FocusOnUpdate() {
            TweetContent.Focus();
        }

        public string UpdateText {
            get { return TweetContent.Text; } 
            set { TweetContent.Text = value; }
        }

        void OnSend(object Sender, RoutedEventArgs Args) {
        	StatusPresenter.Update();
        }

        public void OnReply(object Sender, RoutedEventArgs Args) {
            var Tweet = (Args as Events.Reply.Args).Tweet;
            StatusPresenter.ReplyTo(Tweet);
        }

        public void OnToggleVisibility(object Sender, RoutedEventArgs Args) {
            StatusPresenter.ToggleUpdateVisibility();
        }
    }
}