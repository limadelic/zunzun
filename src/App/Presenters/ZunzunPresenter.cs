using Zunzun.App.Events;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class ZunzunPresenter {
    
        public UserService UserService { get; set; }
        public ZunzunView View { get; set; }
        
        public void Follow(string UserName) {
            UserService.Follow(UserName);
            UserChanged.With(UserName, View);
        }

        public void Unfollow(string UserName) {
            UserService.Unfollow(UserName);
            UserChanged.With(UserName, View);
        }
    }
}