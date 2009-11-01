using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs {
    
    [TestClass]
    public class when_updating_Status : BehaviorOf<StatusPresenter> {
        
        [TestMethod]
        public void should_request_a_Status_Update() {
            var SuperCoolTweet = new TweetClass {Content = "SuperCool"};

            When.Update(SuperCoolTweet);
            Then.TweetService.Should().UpdateStatus(SuperCoolTweet);
        }
    }
}