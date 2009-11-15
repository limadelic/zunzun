using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class UserServiceClass : UserService {
    
        public User FindByUserName(string UserName) { return 
            Request(UserByUserName(UserName))
        ;}

        public virtual ITwitterLeafNode UserByUserName(string UserName) { return 
            FluentTwitter.CreateRequest()
            .Users().ShowProfileFor(UserName)
            .AsJson()
        ;}

        User Request(ITwitterLeafNode Spec) { return 
            Spec.Request().AsUser().ToUser()
        ;}
    }
}