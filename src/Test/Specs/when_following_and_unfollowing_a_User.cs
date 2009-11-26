using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_following_and_unfollowing_a_User : BehaviorOf<UserServiceClass> {
        
        const string UserName = Actors.ZunzunUserName;
        readonly ITwitterLeafNode Spec = TestObjectFor<ITwitterLeafNode>();
        
        [TestMethod]
        public void should_post_a_Follow_request() {
            
            Given.FollowUserSpec(UserName).Is(Spec);
            When.Follow(UserName);
            Spec.Should().Request();
        }
        
        [TestMethod]
        public void should_post_an_Unfollow_request() {
            
            Given.UnfollowUserSpec(UserName).Is(Spec);
            When.Unfollow(UserName);
            Spec.Should().Request();
        }
        
        [TestMethod]
        public void should_retrieve_the_Users_that_are_being_followed() {
            var FollowingSpec = Create.TestObjectFor<ITwitterLeafNode>();
            var TwoUsers = Actors.TwoUsers;            
            
            Given.FollowingSpec.Is(FollowingSpec);
            Given.RequestUsers(FollowingSpec).WillReturn(TwoUsers);
            
            The.Following.Count.ShouldBe(2);
        }
    }
}