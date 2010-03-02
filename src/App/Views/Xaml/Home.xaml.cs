using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Utils;

namespace Zunzun.App.Views.Xaml {

    public partial class Home : HomeView {
    
        HomePresenter Presenter { get; set; }
        public ObservableCollection<Tweet> HomeTweets { get; set; }

//        public void Insert(List<Tweet> Tweets)
//        {
//            HomeTweets.InsertAtTop(Tweets);
//        }

        public void Show(List<Tweet> Tweets)
        {
            HomeTweets.Clear();
            HomeTweets.InsertAtTop(Tweets);
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