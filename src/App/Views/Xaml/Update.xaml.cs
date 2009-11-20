using System;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {

    public partial class Update : StatusView {
    
        StatusPresenter Presenter { get; set; }

        public Update() {
            Presenter = PresenterFactory.NewStatusPresenter(this);
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
            set
            {
				TweetContent.Text = value;
                TweetContent.SelectionStart = value.Length;
            }
        }

        void OnSend(object Sender, RoutedEventArgs Args) {
        	Presenter.Update();
        }

        public void OnReply(object Sender, RoutedEventArgs Args) {
            var Tweet = (Args as Events.Reply.Args).Tweet;
            Presenter.ReplyTo(Tweet);
            
        }

        public void OnToggleVisibility(object Sender, RoutedEventArgs Args) {
            Presenter.ToggleUpdateVisibility();
        }

        public void OnRetweet(object sender, RoutedEventArgs Args)
        {
            var Tweet = (Args as Retweet.Args).Tweet;
            Presenter.Retweet(Tweet);
        }

        public void OnDirectMessage(object sender, RoutedEventArgs Args)
        {
            var Tweet = (Args as DirectMessage.Args).Tweet;
            Presenter.DirectMessage(Tweet);
        }
    }
}