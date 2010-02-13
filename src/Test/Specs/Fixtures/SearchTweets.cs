using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;
using Given=FluentSpec.Given;

namespace Zunzun.Specs.Fixtures {

    public class SearchTweets {

        readonly SearchPresenter SearchPresenter;
        readonly SearchView SearchView;
        
        public SearchTweets() {
            SearchView = Create.TestObjectFor<SearchView>();
            Given.That(SearchView).SearchText.Is("lol");
            
            SearchPresenter = PresenterFactory.NewSearchPresenter(SearchView);
        }

        public bool It_should_find_Tweets() { return Verify.That(() => {
            SearchPresenter.Search();
            SearchView.Tweets.ShouldNotBeEmpty();
        });}
    }
}