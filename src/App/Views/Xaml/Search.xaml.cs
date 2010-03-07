using System.Collections.Generic;
using System.Windows;
using Zunzun.App.Converters;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Search : SearchView {
    
        public List<Tweet> Tweets { get; set; }

        public new bool IsVisible { 
            get { return base.IsVisible; } 
            set { this.IsVisibleIf(value); } 
        }

        public string SearchText { get { return SearchTXT.Text; } }

        SearchPresenter Presenter { get; set; }

        public Search() {
            Presenter = PresenterFactory.NewSearchPresenter(this);
            InitializeComponent();
        }

        void OnSearch(object sender, RoutedEventArgs e) {
            Presenter.Search();
        }

        public void OnToggleVisibility(object Sender, RoutedEventArgs Args) {
            Presenter.ToggleUpdateVisibility();
        }
    }
}
