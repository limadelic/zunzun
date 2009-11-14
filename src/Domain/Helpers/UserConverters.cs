using Dimebrain.TweetSharp.Model;

namespace Zunzun.Domain.Helpers {

    public static class UserConverters {
        
        public static User ToUser(this TwitterUser User) {
            return ObjectFactory.NewUser(User);
        }
    }
}