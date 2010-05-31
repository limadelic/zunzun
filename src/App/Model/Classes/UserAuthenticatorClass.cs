using System;
using Zunzun.Domain;

namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public UserService UserService { get; set; }
        
        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(Domain.Settings.UserName)
            && !string.IsNullOrEmpty(Domain.Settings.Password)
        ;}}

        public void Authenticate(string UserName, string Password) {
        
            if (!UserService.AreValid(UserName, Password)) 
                throw new Exception();
                
            ApplyCredentials(UserName, Password);
        }

        void ApplyCredentials(string UserName, string Password) {
        
            Domain.Settings.UserName = UserName;
            Domain.Settings.Password = Password;
        }
    }
}