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

        void Reply(object sender, RoutedEventArgs e) {
        }

        void Load(object sender, RoutedEventArgs e) {
            HomePresenter.Load();
        }
    }
}