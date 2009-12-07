using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs
{
    [TestClass]
    public class when_showing_a_conversation : BehaviorOf<ConversationPresenter>
    {
        readonly Tweet origTweet = Actors.TweetWithUserAndId;

        [TestMethod]
        public void should_contain_original_Tweet()
        {
            var place = "";
            When.GetConversation(origTweet).ShouldContain(origTweet);
        }

        [TestMethod]
        public void should_contain_replies_to_original_Tweet()
        {
            Given.TweetService.Tweets.WillReturn(Actors.ListOfTweetsWithTwoReplies);

            When.GetConversation(origTweet).Count.ShouldBe(3);
            When.GetConversation(origTweet).ShouldContain(new TweetClass{Content = "firstReply"});
            When.GetConversation(origTweet).ShouldContain(new TweetClass{Content = "secondReply"});
        }
    }
}