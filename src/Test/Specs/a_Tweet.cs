using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs {
    [TestClass]
    public class a_Tweet : BehaviorOf<TweetClass> {

        [TestMethod]
        public void should_be_equal_by_Content() {
        
            var OneTweet = Domain.ObjectFactory.NewTweet(" stuff here ");
            var AnotherTweet = Domain.ObjectFactory.NewTweet(" stuff here ");

            OneTweet.ShouldBe(AnotherTweet);
        }
    }
}