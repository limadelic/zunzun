using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Domain.Classes;

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
    }
}