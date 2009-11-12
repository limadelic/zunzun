using System;
using System.Windows;
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
            View.IsUpdateVisible = !View.IsUpdateVisible;
        }

        public void ReplyTo(Tweet tweet)
        {
            if(!View.IsUpdateVisible) ToggleUpdateVisibility();
            View.UpdateText = reply + tweet.ScreenName;
            View.FocusOnUpdate();
        }
    }
}