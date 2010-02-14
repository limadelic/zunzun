using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Home : HomeView {
    
        HomePresenter Presenter { get; set; }
        public ObservableCollection<Tweet> Tweets { get; set; }

        public Home() {
            Setup();
            InitializeComponent();
        }

        void Setup() {
            Presenter = PresenterFactory.NewHomePresenter(this);
            Tweets = new ObservableCollection<Tweet>();
        }

        void OnLoad(object Sender, RoutedEventArgs Args) {
            if (Zunzun.App.Settings.IsInDesignMode) return;
            
            Presenter.Load();
        }

        public void OnNewTweets(object Sender, RoutedEventArgs Args) {
            var NewTweets = (Args as TweetsEvent.Args).Tweets;
            
            Presenter.Add(NewTweets);
        }
    }
}