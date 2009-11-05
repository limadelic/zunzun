using System.Diagnostics;
using System.Threading;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class UpdateStatus : Spec {

        readonly TweetService TweetService;
        readonly StatusPresenter StatusPresenter = new StatusPresenter();
        private Tweet tweet;

        public UpdateStatus() {
            StatusPresenter = PresenterFactory.NewStatusPresenter();
            TweetService = StatusPresenter.TweetService;
        }

        protected override void SetUpSteps() {
        
            When["Status is updated"] = () => {
                tweet = Actors.SuperCoolTweet;
                StatusPresenter.Update(tweet);
            };

            Then["Home should contain the Tweet"] = () => {
                TweetService.Tweets.ShouldContain(tweet);
            };
        }
    }
}