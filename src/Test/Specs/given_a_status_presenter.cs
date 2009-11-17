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
                Then.View.UpdateText = "@testuser";
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
        }
    }
}