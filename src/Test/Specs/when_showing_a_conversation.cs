using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs
{
    [TestClass]
    public class when_showing_a_conversation : BehaviorOf<HomePresenter>
    {
        readonly Tweet origTweet = Actors.TweetWithUserAndId;

        [TestMethod]
        public void should_contain_original_Tweet()
        {
            Given.View.HomeTweets.Is(Actors.ListOfTweetsWithTwoReplies);
            When.ShowConversation(origTweet);
            The.View.ConvoTweets.ToList().ShouldContain(origTweet);
        }

        [TestMethod]
        public void should_contain_replies_to_original_Tweet()
        {
            Given.View.HomeTweets.Is(Actors.ListOfTweetsWithTwoReplies);
            When.ShowConversation(origTweet);
            The.View.ConvoTweets.Count.ShouldBe(3);
        }

        [TestMethod]
        public void should_have_Tweets_with_multiple_levels_of_reply()
        {
            Given.View.HomeTweets.Is(Actors.ListOfTweetsWithReplyHierarchy);
            When.ShowConversation(origTweet);
            The.View.ConvoTweets.Count.ShouldBe(6);
        }

        [TestMethod]
        public void should_contain_all_the_Tweets_in_the_conversation_if_passed_nonroot_Tweet()
        {
            var list = Given.View.HomeTweets = Actors.ListOfTweetsWithReplyHierarchy;
            When.ShowConversation(list[5]);
            The.View.ConvoTweets.Count.ShouldBe(6);
        }

        [TestMethod]
        public void should_hide_home_when_conversation_is_picked()
        {
            When.ShowConversation(Actors.TweetWithUser);
            Should.View.MakeConversationVisible();
        }
    }
}