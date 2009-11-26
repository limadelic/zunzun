using System.Collections.Generic;
using Dimebrain.TweetSharp.Fluent;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    public class when_following_and_unfollowing_a_User {
        
        const string UserName = Actors.ZunzunUserName;
        
        [TestClass]
        public class a_ZunzunPresenter : BehaviorOf<ZunzunPresenter> {

            [TestMethod]
            public void should_delegate_Follow_to_service() {

                When.Follow(UserName);
                Then.UserService.Should().Follow(UserName);
            }

            [TestMethod]
            public void should_delegate_Unfollow_to_service() {

                When.Unfollow(UserName);
                Then.UserService.Should().Unfollow(UserName);
            }
        }
        
        [TestClass]
        public class a_UserPresenter : BehaviorOf<UserHomePresenter> {
        
            [TestInitialize]
            public void SetUp() {
                Given.UserService.FindByUserName(UserName)
                .WillReturn(Actors.Zunzun);
            }

            [TestMethod]
            public void should_not_allow_to_Follow_users_already_being_followed() {
            
                Given.UserService.Following.Are(Actors.TwoUsers);

                When.Show(UserName);

                Then.View.AllowToFollow = false;
                Then.View.AllowToUnfollow = true;
            }

            [TestMethod]
            public void should_allow_to_Follow_new_users() {
            
                Given.UserService.Following.Are(new List<User>());

                When.Show(UserName);

                Then.View.AllowToFollow = true;
                Then.View.AllowToUnfollow = false;
            }
        }

        [TestClass]
        public class a_UserService : BehaviorOf<UserServiceClass> {

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
}