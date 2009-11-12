using Dimebrain.TweetSharp.Model;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures {

    public class ReplyToTweet : Spec {
    
        Tweet Tweet;
        readonly StatusView View;
        readonly StatusPresenter Presenter;

        public ReplyToTweet() {
            View = Create.TestObjectFor<StatusView>();
            Presenter = PresenterFactory.NewStatusPresenter(View);
        }

        protected override void SetUpSteps() {
        
            Given("a tweet by user {0}", UserName => {
                var status = new TwitterStatus {User = new TwitterUser {Name = UserName}};
                Tweet = ObjectFactory.NewTweet(status);
            });

            When("I reply to the Tweet", () => Presenter.ReplyTo(Tweet));

            Then("Update Text contains {0}", ReplyPrefix => 
                View.UpdateText.ShouldContain(ReplyPrefix));
        }
    }
}