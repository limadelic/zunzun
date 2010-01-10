using System.Collections.Generic;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Model;

namespace Zunzun.Domain.Helpers {

    public static class UserConverters {
        
        public static User ToUser(this TwitterUser User) {
            return ObjectFactory.NewUser(User);
        }
 
        public static IEnumerable<User> ToUsers(this TwitterResult Request) {
            foreach (var User in Request.AsUsers()) 
                yield return User.ToUser();
        }
   }
}