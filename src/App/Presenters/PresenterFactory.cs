using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public static class PresenterFactory {

        public static HomePresenter NewHomePresenter(HomeView View) {
            return new HomePresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService
            };
        }

        public static StatusPresenter NewStatusPresenter(StatusView View) {
            return new StatusPresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService
            };
        }

        public static UserHomePresenter NewUserHomePresenter(UserHomeView View) { 
            return new UserHomePresenter {
                View = View,
            };
        }
    }
}