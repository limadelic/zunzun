using System;
using System.Linq;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class UpdateStatus : Spec {

        readonly TweetService TweetService;
        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        private long OrigId;
        private Tweet Tweet;

        public UpdateStatus() {
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
            TweetService = UpdateStatusPresenter.TweetService;
        }

        protected override void SetUpSteps() {
        
            Given("a tweet by user {0}", UserName => Tweet = new TweetClass {
                Author = new UserClass { UserName = UserName }
            });

            Given("a tweet by user {0} with content {1}", (UserName, Content) => Tweet = new TweetClass {
                Id = OrigId = new Random().Next(),
                Content = Content,
                Author = new UserClass { UserName = UserName }
            });

            When("I reply to the Tweet", () => UpdateStatusPresenter.ReplyTo(Tweet) );

            When("I send the user a Direct Message", () => UpdateStatusPresenter.DirectMessage(Tweet) );

            When("I retweet it", () => UpdateStatusPresenter.Retweet(Tweet));

            When("Status is updated", () => {
                Tweet = Actors.UniqueTweet;
                UpdateStatusPresenter.Update(Tweet);
            });

            And("I submit my update", () => UpdateStatusPresenter.Update());

            Then("Home should contain the Tweet", () => 
                TweetService.Tweets.ToList().ShouldContain(Tweet));

            Then("Update text starts with {0}", Contents => 
                UpdateStatusView.UpdateText.StartsWith(Contents).ShouldBeTrue());

            Then("my Tweet should be linked to the original Tweet", () => {
                var SentTweet = TweetService.Tweets.Where(Tweet => 
                    Tweet.Author.UserName.Equals(Settings.UserName)).First();
                    
                SentTweet.ReplyTo.ShouldBe(OrigId);
            });
        }
    }
}