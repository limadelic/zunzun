using System;
using System.Linq;
using Dimebrain.TweetSharp.Model;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures {

    public class UpdateStatus : Spec {

        readonly TweetService TweetService;
        readonly StatusPresenter StatusPresenter;
        readonly StatusView StatusView;
        private long origId;
        private Tweet Tweet;

        public UpdateStatus() {
            StatusView = Create.TestObjectFor<StatusView>();
            StatusPresenter = PresenterFactory.NewStatusPresenter(StatusView);
            TweetService = StatusPresenter.TweetService;
        }

        protected override void SetUpSteps() {
        
            Given("a tweet by user {0}", UserName =>
            {
                var status = new TwitterStatus { Id = origId = new Random().Next() , User = new TwitterUser { ScreenName = UserName }};
                Tweet = ObjectFactory.NewTweet(status);
            });

            When("I reply to the Tweet", () => StatusPresenter.ReplyTo(Tweet) );

            When("I send the user a Direct Message", () => StatusPresenter.DirectMessage(Tweet) );

            When("I retweet it", () => StatusPresenter.Retweet(Tweet));

            When("Status is updated", () => {
                Tweet = Actors.UniqueTweet;
                StatusPresenter.Update(Tweet);
            });

            And("I submit my update", () => StatusPresenter.Update());

            Then("Home should contain the Tweet", () => 
                TweetService.Tweets.ToList().ShouldContain(Tweet));

            Then("Update text starts with {0}", Contents => Assert.IsTrue(StatusView.UpdateText.StartsWith(Contents)));

            Then("my tweet should be linked to tweet {0}", TweetId =>
            {
                var SentTweet = TweetService.Tweets.Where(x => x.ScreenName.Equals("kinobot")).First();
                SentTweet.ReplyTo.ShouldBe(origId);
            });
        }
    }
}