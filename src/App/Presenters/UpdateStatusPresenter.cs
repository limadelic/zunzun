using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UpdateStatusPresenter {
    
        private const string RetweetPrefix = "RT";
        private const string ReplyPrefix = "@";
        private const string DirectMessagePrefix = "D";

        public UpdateStatusView View { get; set; }
        public TweetService TweetService { get; set; }
        public UrlShrinker UrlShrinker { get; set; }

        public long AssociatedTweetId { get; set; }

        public void Update(Tweet tweet)
        {
            if (HasAssociatedTweet)
            {
                tweet.ReplyTo = AssociatedTweetId;
                TweetService.SendReply(tweet);
            }
            else
                TweetService.UpdateStatus(tweet);
        }

        protected bool HasAssociatedTweet { get { return AssociatedTweetId > 0; } }

        public void Update() {
            if (string.IsNullOrEmpty(View.UpdateText)) return;

            var tweet = Domain.ObjectFactory.NewTweet(View.UpdateText);

            Update(tweet);
            
            ClearUpdateText();
        }

        void ClearUpdateText() { View.UpdateText = string.Empty; }

        public void ToggleUpdateVisibility() {
            View.IsVisible = !View.IsVisible;
        }

        public void ReplyTo(Tweet tweet)
        {
            AssociatedTweetId = tweet.Id;
            FocusOnUpdate();
            View.UpdateText = ReplyPrefix + tweet.Author.UserName + " ";
        }

        void FocusOnUpdate()
        {
            if(!View.IsVisible) ToggleUpdateVisibility();
            View.FocusOnUpdate();
        }

        public void Retweet(Tweet tweet)
        {
            FocusOnUpdate();
            View.UpdateText = RetweetPrefix + " " + ReplyPrefix + tweet.Author.UserName + " " + tweet.Content + " ";
        }

        public void DirectMessage(Tweet tweet)
        {
            FocusOnUpdate();
            View.UpdateText = DirectMessagePrefix + " " + tweet.Author.UserName + " ";
        }

        public void UpdateTextChanged() {
            if (!View.UpdateText.EndsWith(" ")) return;

            ShortenUrls();
        }

        void ShortenUrls() {
            View.UpdateText = UrlShrinker.Shorten(View.UpdateText);
        }

        public void UpdateTextPasted() {
            ShortenUrls();
        }
    }
}