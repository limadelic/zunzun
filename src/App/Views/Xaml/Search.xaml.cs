using System.Collections.Generic;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Search : SearchView {
    
        public List<Tweet> Tweets { get; set; }

        public string SearchText { get { return SearchTXT.Text; } }

        SearchPresenter Presenter { get; set; }

        public Search() {
            Presenter = PresenterFactory.NewSearchPresenter(this);
            InitializeComponent();
        }

        void OnSearch(object sender, System.Windows.RoutedEventArgs e) {
            Presenter.Search();
        }

    }
}
