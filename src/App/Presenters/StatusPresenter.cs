using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class StatusPresenter {
        private const string reply = "@";

        public StatusView View { get; set; }

        public TweetService TweetService { get; set; }

        public void Update(Tweet Tweet) { TweetService.UpdateStatus(Tweet); }

        public void Update() {
            if (string.IsNullOrEmpty(View.UpdateText)) return;

            var Tweet = Domain.ObjectFactory.NewTweet(View.UpdateText);

            Update(Tweet);
            
            ClearUpdateText();
        }

        void ClearUpdateText() { View.UpdateText = string.Empty; }

        public void ToggleUpdateVisibility() {
            View.IsVisible = !View.IsVisible;
        }

        public void ReplyTo(Tweet Tweet) {
            if(!View.IsVisible) ToggleUpdateVisibility();
            View.UpdateText = reply + Tweet.ScreenName;
            View.FocusOnUpdate();
        }
    }
}