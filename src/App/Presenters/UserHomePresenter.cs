using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UserHomePresenter {
    
        public UserHomeView View { get; set; }
        public UserService UserService { get; set; }

        public void Show(string UserName) {
            View.User = UserService.FindByUserName(UserName);
        }
    }
}