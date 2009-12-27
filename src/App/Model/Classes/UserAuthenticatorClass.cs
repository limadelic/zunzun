using System;
using Zunzun.Domain;

namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public UserService UserService { get; set; }
        
        public virtual string UserName {
            get { return Domain.Settings.UserName; }
            set { Domain.Settings.UserName = value;}
        }

        public virtual string Password {
            get { return Domain.Settings.Password; }
            set { Domain.Settings.Password = value;}
        }

        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(UserName)
            && !string.IsNullOrEmpty(Password)
        ;}}

        public void Authenticate(string UserName, string Password) {
        
            if (!UserService.AreValid(UserName, Password)) 
                throw new Exception();
                
            ApplyCredentials(UserName, Password);
        }

        void ApplyCredentials(string UserName, string Password) {
        
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}