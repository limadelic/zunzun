using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public static class PresenterFactory {
    
        public static HomePresenter NewHomePresenter(HomeView View) { 
            return new HomePresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService
            };
        }

        public static StatusPresenter NewStatusPresenter() {
            return new StatusPresenter {
                TweetService = Domain.ObjectFactory.NewTweetService
            };
        }
    }
}