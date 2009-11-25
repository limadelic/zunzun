using System.Collections.Generic;
using System.Diagnostics;
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

        public void Follow(string UserName) {
            FollowUserSpec(UserName).Request()
        ;}
        
        public List<User> Following { get { return null; } }
        
        public virtual ITwitterLeafNode FollowUserSpec(string UserName) { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Friendships().Befriend(UserName)
            .AsJson()
        ;}
    }
}