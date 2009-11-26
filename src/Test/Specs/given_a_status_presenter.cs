using System;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;
using ObjectFactory = Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs {
    
    [TestClass]
    public class given_a_status_presenter {

        [TestClass]
        public class when_updating_Status : BehaviorOf<StatusPresenter>
        {
            [TestMethod]
            public void should_request_a_Status_Update()
            {
                var SuperCoolTweet = new TweetClass { Content = new Guid().ToString() };

                When.Update(SuperCoolTweet);
                Then.TweetService.Should().UpdateStatus(SuperCoolTweet);
            }

            [TestMethod]
            public void should_create_a_Tweet_based_on_view()
            {

                Given.View.UpdateText = "very cool";
                When.Update();
                Then.TweetService.Should().UpdateStatus(ObjectFactory.NewTweet("very cool"));
            }

            [TestMethod]
            public void should_not_send_empty_updates()
            {

                Given.View.UpdateText = string.Empty;
                When.Update();
                Then.TweetService.ShouldNot().IgnoringArgs().UpdateStatus(null);
            }

            [TestMethod]
            public void should_clear_view_after_Updating()
            {

                Given.View.UpdateText = "Content";
                When.Update();
                Should.View.UpdateText = string.Empty;
            }

            [TestMethod]
            public void should_toggle_visibility()
            {

                Given.View.IsVisible = true;

                When.ToggleUpdateVisibility();
                Then.View.IsVisible.ShouldBeFalse();

                When.ToggleUpdateVisibility();
                Then.View.IsVisible.ShouldBeTrue();
            }
        }

        [TestClass]
        public class when_replying_to_a_Tweet : BehaviorOf<StatusPresenter>
        {
            [TestMethod]
            public void should_set_update_text_to_the_User()
            {
                When.ReplyTo(Actors.TweetWithUser);
                Then.View.UpdateText = "@zunzunapp ";
            }

            [TestMethod]
            public void should_make_Update_visible_if_hidden()
            {
                Given.View.IsVisible = false;
                When.ReplyTo(Actors.TweetWithUser);
                Then.View.IsVisible.ShouldBeTrue();
            }

            [TestMethod]
            public void should_focus_on_Update()
            {
                When.ReplyTo(Actors.TweetWithUser);
                Should.View.FocusOnUpdate();
            }

            [TestMethod]
            public void should_associate_Tweet_with_Original()
            {
                var Tweet = Actors.TweetWithUserAndId;
                When.ReplyTo(Tweet);
                Should.AssociatedTweetId = Tweet.Id;
            }

            [TestMethod]
            public void should_request_a_Reply_if_associated_Tweet_Exists()
            {
                Given.AssociatedTweetId = 42;
                var ReplyTweet = ObjectFactory.NewTweet("Poop");

                When.Update(ReplyTweet);

                Then.TweetService.Should().SendReply(ReplyTweet, 42);
            }

//            [TestMethod]
//            public void should_attach_to_original_Tweet()
//            {
//                var OrigTweet = Actors.TweetWithUserAndId;
//                var ReplyTweet = ObjectFactory.NewTweet("@testuser ");
//
//                When.ReplyTo(OrigTweet);
//                When.Update(ReplyTweet);
//
//                ReplyTweet.ReplyTo.ShouldBe(OrigTweet.Id);
//            }
        }

        [TestClass]
        public class when_retweeting : BehaviorOf<StatusPresenter>
        {
            [TestMethod]
            public void should_set_update_text_to_retweet_standards()
            {
                When.Retweet(Actors.TweetWithUserAndContent);
                Then.View.UpdateText = "RT @zunzunapp and now for something completely different ";
            }

            [TestMethod]
            public void should_make_Update_visible_if_hidden()
            {
                Given.View.IsVisible = false;
                When.Retweet(Actors.TweetWithUser);
                Then.View.IsVisible.ShouldBeTrue();
            }

            [TestMethod]
            public void should_focus_on_Update()
            {
                When.Retweet(Actors.TweetWithUser);
                Should.View.FocusOnUpdate();
            }
        }

        [TestClass]
        public class when_direct_messaging : BehaviorOf<StatusPresenter>
        {
            [TestMethod]
            public void should_set_update_text_to_directmessage_standards()
            {
                When.DirectMessage(Actors.TweetWithUser);
                Then.View.UpdateText = "D zunzunapp ";
            }

            [TestMethod]
            public void should_make_Update_visible_if_hidden()
            {
                Given.View.IsVisible = false;
                When.DirectMessage(Actors.TweetWithUser);
                Then.View.IsVisible.ShouldBeTrue();
            }

            [TestMethod]
            public void should_focus_on_Update()
            {
                When.DirectMessage(Actors.TweetWithUser);
                Should.View.FocusOnUpdate();
            }
        }
    }
}