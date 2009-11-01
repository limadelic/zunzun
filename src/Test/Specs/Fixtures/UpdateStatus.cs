using Zunzun.App;
using Zunzun.App.Presenters;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class UpdateStatus : Spec {

        readonly TweetService TweetService;
        readonly StatusPresenter StatusPresenter = new StatusPresenter();

        public UpdateStatus() {
            StatusPresenter = PresenterFactory.NewStatusPresenter();
            TweetService = StatusPresenter.TweetService;
        }

        protected override void SetUpSteps() {
        
            When["Status is updated"] = () => {
                StatusPresenter.Update(Actors.SuperCoolTweet);
            };

            Then["Home should contain the Tweet"] = () => {
                TweetService.Tweets.ShouldContain(Actors.SuperCoolTweet);
            };
        }
    }
}