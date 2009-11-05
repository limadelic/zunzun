using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class StatusPresenter {
    
        public StatusView View { get; set; }

        public TweetService TweetService { get; set; }

        public void Update(Tweet Tweet) {
            TweetService.UpdateStatus(Tweet);
        }

        public void Update() {
            if (string.IsNullOrEmpty(View.UpdateText)) return;
            
            Update(Domain.ObjectFactory.NewTweet(View.UpdateText));
        }
    }
}