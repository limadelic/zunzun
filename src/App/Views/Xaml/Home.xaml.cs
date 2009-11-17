using System.Collections.ObjectModel;
using System.Windows;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Home : HomeView {
    
        HomePresenter HomePresenter { get; set; }
        public ObservableCollection<Tweet> Tweets { get; set; }      
          
        public Home() {
            HomePresenter = PresenterFactory.NewHomePresenter(this);
            Tweets = new ObservableCollection<Tweet>();
            InitializeComponent();
        }

//        void OnReply(object Sender, RoutedEventArgs Args) {
//            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
//            Events.Reply.To(Tweet, Sender);
//        }

        void OnLoad(object Sender, RoutedEventArgs Args) {
            if (Settings.IsInDesignMode) return;
            
            HomePresenter.Load();
        }
    }
}