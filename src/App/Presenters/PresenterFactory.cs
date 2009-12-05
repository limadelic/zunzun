using Zunzun.App.Views;

namespace Zunzun.App.Presenters {

    public static class PresenterFactory {

        public static ZunzunPresenter NewZunzunPresenter(ZunzunView View) {
            
            var Presenter = new ZunzunPresenter {
                View = View,
                UserService = Domain.ObjectFactory.NewUserService,
                UserAuthenticator = ObjectFactory.NewUserAuthenticator
            };
            
            Presenter.RegisterEvents();
            
            return Presenter;
        }

        public static HomePresenter NewHomePresenter(HomeView View) {
            return new HomePresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService,
                Timer = ObjectFactory.NewTimer
            };
        }

        public static UpdateStatusPresenter NewStatusPresenter(UpdateStatusView View) {
            return new UpdateStatusPresenter {
                View = View,
                TweetService = Domain.ObjectFactory.NewTweetService,
                UrlShrinker = Domain.ObjectFactory.NewUrlShrinker
            };
        }

        public static UserHomePresenter NewUserHomePresenter(UserHomeView View) { 
            return new UserHomePresenter {
                View = View,
                UserService = Domain.ObjectFactory.NewUserService,
                TweetService = Domain.ObjectFactory.NewTweetService,
            };
        }

        public static LoginPresenter NewLoginPresenter(LoginView View) {
            return new LoginPresenter {
                View = View, 
                UserAuthenticator = ObjectFactory.NewUserAuthenticator
            };
        }
    }
}