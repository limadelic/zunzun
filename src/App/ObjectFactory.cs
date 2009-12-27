using Zunzun.App.Model;
using Zunzun.App.Model.Classes;

namespace Zunzun.App {

    public static class ObjectFactory {
    
        public static TextFormatter NewTextFormatter { get { return new 
            TextFormatterClass()
        ;}}

        public static Timer NewTimer { get { return new TimerClass(); }}
        
        public static UserAuthenticator NewUserAuthenticator { get { return new UserAuthenticatorClass {
            UserService = Domain.ObjectFactory.NewUserService
        };}}
    }
}