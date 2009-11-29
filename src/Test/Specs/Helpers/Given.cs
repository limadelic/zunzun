namespace Zunzun.Specs.Helpers {

    public static class Given {
        
        public static void Credentials(string UserName, string Password) {
            Domain.Settings.UserName = UserName;
            Domain.Settings.Password = Password;
        }
    }
}