using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;
using ObjectFactory = Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs
{
    [TestClass]
    public class when_showing_a_conversation
    {
        [TestClass]
        public class the_Conversation : BehaviorOf<ConversationClass>
        {
            readonly Tweet origTweet = Actors.TweetWithUserAndId;

            [TestMethod]
            public void should_contain_original_Tweet()
            {
                Given.Tweets = Actors.ListOfTweetsWithTwoReplies;
                The.ConstructConversation(origTweet).ShouldContain(origTweet);
            }

            [TestMethod]
            public void should_contain_replies_to_original_Tweet()
            {
                Given.Tweets = Actors.ListOfTweetsWithTwoReplies;
                The.ConstructConversation(origTweet).Count.ShouldBe(3);
            }

            [TestMethod]
            public void should_have_Tweets_with_multiple_levels_of_reply()
            {
                Given.Tweets = Actors.ListOfTweetsWithReplyHierarchy;
                The.ConstructConversation(origTweet).Count.ShouldBe(6);
            }

            [TestMethod]
            public void should_contain_all_the_Tweets_in_the_conversation_if_passed_nonroot_Tweet()
            {
                var list = Actors.ListOfTweetsWithReplyHierarchy;
                Given.Tweets = list;

                The.ConstructConversation(list[5]).Count.ShouldBe(6);
            }
        }

        [TestClass]
        public class the_Presenter : BehaviorOf<HomePresenter> {
            
            [TestMethod]
            public void should_clear_the_View() {
                When.ShowConversation(Actors.UniqueTweet);
                Then.Should().View.Tweets.Clear();
            }

            [TestMethod]
            public void should_constuct_conversation() {
                var Tweet = Actors.UniqueTweet;
                var List = Actors.ListOfTweetsWithTwoReplies;

                Given.TweetService.Tweets.WillReturn(List);
                When.ShowConversation(Tweet);
                Then.Should().Add(ObjectFactory.NewConversation(List).ConstructConversation(Tweet));
            }
        }
    }
}