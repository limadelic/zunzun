using System;
using System.Collections.Generic;
using System.Linq;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Fluent;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class UserServiceClass : UserService {
    
        public User FindByUserName(string UserName) { return 
            Request(UserByUserNameSpec(UserName))
        ;}

        User Request(ITwitterLeafNode Spec) { return 
            Spec.Request().AsUser().ToUser()
        ;}

        public virtual List<User> RequestUsers(ITwitterLeafNode Spec) { return 
            Spec.Request().ToUsers().ToList()
        ;}

        public List<User> Following { get { return 
            RequestUsers(FollowingSpec)
        ;}}

        public void Follow(string UserName) {
            FollowUserSpec(UserName).Request()
        ;}
        
        public void Unfollow(string UserName) {
            UnfollowUserSpec(UserName).Request()
        ;}

        public bool AreValid(string UserName, string Password) { return 
            AreValidCredentialsSpec(UserName, Password)
                .Request().AsUser() != null
        ;}

        #region Specs

        public virtual ITwitterLeafNode UserByUserNameSpec(string UserName) { return 
            TwitterAPI.Users.ShowProfileFor(UserName).AsJson()
        ;}
        
        public virtual ITwitterLeafNode FollowingSpec { get { return 
            TwitterAPI.Friends.AsJson()
        ;}}

        public virtual ITwitterLeafNode FollowUserSpec(string UserName) { return 
            TwitterAPI.Friendships.Befriend(UserName).AsJson()
        ;}

        public virtual ITwitterLeafNode UnfollowUserSpec(string UserName) { return 
            TwitterAPI.Friendships.Destroy(UserName).AsJson()
        ;}
        
        public virtual ITwitterLeafNode AreValidCredentialsSpec(string UserName, string Password) { return 
            TwitterAPI.Request.AuthenticateAs(UserName, Password)
            .Account().VerifyCredentials().AsJson()            
        ;}
        
        #endregion
    }
}