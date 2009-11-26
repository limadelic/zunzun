using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual List<User> RequestUsers(ITwitterLeafNode Spec) { return 
            Spec.Request().ToUsers().ToList()
        ;}

        public void Follow(string UserName) {
            FollowUserSpec(UserName).Request()
        ;}
        
        public List<User> Following { get { return 
            RequestUsers(FollowingSpec)
        ;}}

        public void Unfollow(string UserName) { 
        }

        public virtual ITwitterLeafNode FollowingSpec { get { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Users().GetFriends().AsJson()
        ;}}

        public virtual ITwitterLeafNode FollowUserSpec(string UserName) { return 
            FluentTwitter.CreateRequest()
            .AuthenticateAs(Settings.UserName, Settings.Password)
            .Friendships().Befriend(UserName).AsJson()
        ;}
    }
}