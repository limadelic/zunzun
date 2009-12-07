using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
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
            Given.TweetService.Tweets.WillReturn(Actors.ListOfTweetsWithTwoReplies);
            When.GetConversation(origTweet).ShouldContain(origTweet);
        }

        [TestMethod]
        public void should_contain_replies_to_original_Tweet()
        {
            Given.TweetService.Tweets.WillReturn(Actors.ListOfTweetsWithTwoReplies);
            When.GetConversation(origTweet).Count.ShouldBe(3);
        }

        [TestMethod]
        public void should_have_Tweets_with_multiple_levels_of_reply()
        {
            Given.TweetService.Tweets.WillReturn(Actors.ListOfTweetsWithReplyHierarchy);
            When.GetConversation(origTweet).Count.ShouldBe(6);
        }

//        [TestMethod]
//        public void should_contain_the_Tweet_that_the_original_is_replying_to()
//        {
//            Given.TweetService.Tweets.WillReturn(Actors.ListOfTweetsWithTwoReplies);
//            When.GetConversation(Actors.ReplyingTweet).ShouldContain(origTweet);
//        }
    }
}