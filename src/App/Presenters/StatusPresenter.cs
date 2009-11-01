using Zunzun.Domain;

namespace Zunzun.App.Presenters {
    public class StatusPresenter {

        public TweetService TweetService { get; set; }

        public void Update(Tweet Tweet) {
            TweetService.UpdateStatus(Tweet);
        }
    }
}