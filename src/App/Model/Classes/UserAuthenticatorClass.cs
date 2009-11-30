using System;
using Tools;
using Zunzun.Domain;

namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public UserService UserService { get; set; }
        public KeyMaker KeyMaker { get; set; }

        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(Settings.UserName)
            && !string.IsNullOrEmpty(Settings.Password)
        ;}}

        public void Authenticate(string UserName, string Password) {
        
            if (!UserService.AreValid(UserName, Password)) 
                throw new Exception();
                
            Domain.Settings.UserName = UserName;
            Domain.Settings.Password = Password;
            Domain.Settings.EncryptedPassword = KeyMaker.Encrypt(Password);
        }
    }
}