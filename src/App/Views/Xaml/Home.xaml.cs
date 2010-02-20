using System;
using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Home : HomeView {
    
        HomePresenter Presenter { get; set; }
        public ObservableCollection<Tweet> HomeTweets { get; set; }
        public ObservableCollection<Tweet> ConvoTweets { get; set; }
        public void MakeHomeVisible()
        {
            throw new NotImplementedException();
        }

        public void MakeConversationVisible()
        {
            ConvoTweetsLBX.Visibility = Visibility.Visible;
            HomeTweetsLBX.Visibility = Visibility.Collapsed;
        }

        public Home() {
            Setup();
            InitializeComponent();
        }

        void Setup() {
            Presenter = PresenterFactory.NewHomePresenter(this);
            HomeTweets = new ObservableCollection<Tweet>();
        }

        void OnLoad(object Sender, RoutedEventArgs Args) {
            if (Zunzun.App.Settings.IsInDesignMode) return;
            
            Presenter.Load();
        }

        public void OnNewTweets(object Sender, RoutedEventArgs Args) {
            var NewTweets = (Args as TweetsEvent.Args).Tweets;
            
            Presenter.Add(NewTweets);
        }

        public void ShowConversation(object sender, RoutedEventArgs Args)
        {
            Presenter.ShowConversation((Args as TweetEvent.Args).Tweet);
        }
    }
}