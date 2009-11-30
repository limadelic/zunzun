using Zunzun.App.Controls;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {
    
    public partial class Login : LoginView {
    
        LoginPresenter Presenter { get; set;}
    
        public Login() {
            Presenter = PresenterFactory.NewLoginPresenter(this);
            InitializeComponent();
        }

        public string UserName { get { return UserNameTXT.Text; } }
        
        public string Password { get { return PasswordTXT.Password; } }

        public void ShowError() {
            ErrorTXT.Show();
        }

        void OnLogin(object Sender, System.Windows.RoutedEventArgs Args) {
            Presenter.Login();
        }

        void HideError(object Sender, System.Windows.RoutedEventArgs Args) {
            ErrorTXT.Hide();
        }
    }
}
