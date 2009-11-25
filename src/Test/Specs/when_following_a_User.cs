using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_following_a_User : BehaviorOf<UserServiceClass> {
        
        [TestMethod]
        public void should_post_a_Follow_request() {
            var FollowUserSpec = TestObjectFor<ITwitterLeafNode>();
            const string UserName = "zunzun";
            
            Given.FollowUserSpec(UserName).Is(FollowUserSpec);
            When.Follow(UserName);
            FollowUserSpec.Should().Request();
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