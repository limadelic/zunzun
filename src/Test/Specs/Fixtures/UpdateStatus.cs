using System.Linq;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class UpdateStatus : Spec {

        readonly TweetService TweetService;
        readonly StatusPresenter StatusPresenter;
        readonly StatusView StatusView;
        private Tweet Tweet;

        public UpdateStatus() {
            StatusView = Create.TestObjectFor<StatusView>();
            StatusPresenter = PresenterFactory.NewStatusPresenter(StatusView);
            TweetService = StatusPresenter.TweetService;
        }

        protected override void SetUpSteps() {
        
            Given("a tweet by user {0}", UserName => {
                Tweet = new TweetClass {
                    Author = new UserClass { UserName = UserName }
                };
            });

            Given("A tweet by user {0} with content {1}", (UserName, Content) =>
            {
                Tweet = new TweetClass {
                    Content = Content,
                    Author = new UserClass { UserName = UserName }
                };
            });

            When("I reply {0} to the Tweet", Reply => 
            { 
                StatusPresenter.ReplyTo(Tweet);
                StatusView.UpdateText += Reply;
                StatusPresenter.Update();
            });

            When("I reply to the Tweet", () => StatusPresenter.ReplyTo(Tweet));

            When("I send the user a Direct Message", () => StatusPresenter.DirectMessage(Tweet) );

            When("I retweet it", () => StatusPresenter.Retweet(Tweet));

            When("Status is updated", () => {
                Tweet = Actors.UniqueTweet;
                StatusPresenter.Update(Tweet);
            });

            Then("Home should contain the Tweet", () => 
                TweetService.Tweets.ToList().ShouldContain(Tweet));

            Then("Update text starts with {0}", Contents => Assert.IsTrue(StatusView.UpdateText.StartsWith(Contents)));

            Then("{0} should be linked to {1}", (Reply, Original) =>
            {
                var SentTweet = TweetService.Tweets.Where(x => x.Content.Equals(Reply)).First();
                SentTweet.ReplyTo.ShouldBe(Tweet.Id);
            });
        }
    }
}