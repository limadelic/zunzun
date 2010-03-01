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
            Given.TweetCache.Is(Actors.ListOfTweetsWithTwoReplies);
            The.ConstructConversation(origTweet).ShouldContain(origTweet);
        }

        [TestMethod]
        public void should_contain_replies_to_original_Tweet()
        {
            Given.TweetCache.Is(Actors.ListOfTweetsWithTwoReplies);
            The.ConstructConversation(origTweet).Count.ShouldBe(3);
        }

        [TestMethod]
        public void should_have_Tweets_with_multiple_levels_of_reply()
        {
            Given.TweetCache.Is(Actors.ListOfTweetsWithReplyHierarchy);
            The.ConstructConversation(origTweet).Count.ShouldBe(6);
        }

        [TestMethod]
        public void should_contain_all_the_Tweets_in_the_conversation_if_passed_nonroot_Tweet()
        {
            var list = Actors.ListOfTweetsWithReplyHierarchy;
            Given.TweetCache.Is(list);

            The.ConstructConversation(list[5]).Count.ShouldBe(6);
        }
    }
}