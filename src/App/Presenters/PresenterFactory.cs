using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public static class PresenterFactory {

        public static ZunzunPresenter NewZunzunPresenter(ZunzunView View) {
            return new ZunzunPresenter {
                View = View,
                UserService = Domain.ObjectFactory.NewUserService
            };
        }

        public static HomePresenter NewHomePresenter(HomeView View) {
            return new HomePresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService,
                Timer = ObjectFactory.NewTimer
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
                UserService = Domain.ObjectFactory.NewUserService,
                TweetService = Domain.ObjectFactory.NewTweetService,
            };
        }
    }
}