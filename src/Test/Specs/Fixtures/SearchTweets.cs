using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures {

    public class SearchTweets {

        readonly TweetService TweetService = ObjectFactory.NewTweetService;
        List<Tweet> TweetsFound;
        
        readonly HomePresenter HomePresenter;
        readonly HomeView HomeView;
        
        public SearchTweets() {
            HomeView = Create.TestObjectFor<HomeView>();
            HomeView.Tweets = new ObservableCollection<Tweet>();
            
            HomePresenter = PresenterFactory.NewHomePresenter(HomeView);
        }

        public bool zunzun_should_find_Tweets() { return Verify.That(() => 
            (TweetsFound = TweetService.TweetsContaining("lol"))
                .ShouldNotBeEmpty()
        );}
        
        public bool and_place_them_at_the_top_of_Home_tweets() { return Verify.That(() => {
            HomePresenter.Add(TweetsFound);
            HomeView.Tweets[0].ShouldBe(TweetsFound[0]);
        });}
    }
}