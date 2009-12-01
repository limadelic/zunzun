using System;
using Zunzun.Domain;
using Zunzun.Utils;

namespace Zunzun.App.Model.Classes {

    public class UserAuthenticatorClass : UserAuthenticator {
    
        public UserService UserService { get; set; }
        public KeyMaker KeyMaker { get; set; }

        Utils.Properties.Settings UtilsSettings { get { return
            Utils.Properties.Settings.Default
        ;}} 

        public bool HasCredentials { get { return 
            !string.IsNullOrEmpty(UtilsSettings.UserName)
            && !string.IsNullOrEmpty(UtilsSettings.Password)
        ;}}

        public void Authenticate(string UserName, string Password) {
        
            if (!UserService.AreValid(UserName, Password)) 
                throw new Exception();
                
            ApplyCredentials(UserName, Password);
            SaveCredentials();
        }

        public void UseCredentials() {
            Domain.Settings.UserName = UtilsSettings.UserName;
            Domain.Settings.Password = KeyMaker.Decrypt(UtilsSettings.Password);
        }

        void ApplyCredentials(string UserName, string Password) {
        
            Domain.Settings.UserName = UserName;
            Domain.Settings.Password = Password;

            UtilsSettings.UserName = UserName;
            UtilsSettings.Password = KeyMaker.Encrypt(Password);
        }

        public virtual void SaveCredentials() { UtilsSettings.Save(); }
    }
}