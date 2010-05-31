using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class UpdateStatusPresenter {
        bool OnPaste;

        private const string RetweetPrefix = "RT";
        private const string ReplyPrefix = "@";
        private const string DirectMessagePrefix = "D";

        public UpdateStatusView View { get; set; }
        public TweetService TweetService { get; set; }
        public UrlShrinker UrlShrinker { get; set; }

        public long AssociatedTweetId { get; set; }

        public void Update(Tweet Tweet)
        {
            if (HasAssociatedTweet)
            {
                Tweet.ReplyTo = AssociatedTweetId;
                TweetService.SendReply(Tweet);
            }
            else
                TweetService.UpdateStatus(Tweet);
        }

        protected bool HasAssociatedTweet { get { return AssociatedTweetId > 0; } }

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

        public void ReplyTo(Tweet Tweet)
        {
            AssociatedTweetId = Tweet.Id;
            FocusOnUpdate();
            View.UpdateText = ReplyPrefix + Tweet.Author.UserName + " ";
        }

        void FocusOnUpdate()
        {
            if(!View.IsVisible) ToggleUpdateVisibility();
            View.FocusOnUpdate();
        }

        public void Retweet(Tweet Tweet)
        {
            FocusOnUpdate();
            View.UpdateText = RetweetPrefix + " " + ReplyPrefix + Tweet.Author.UserName + " " + Tweet.Content + " ";
        }

        public void DirectMessage(Tweet Tweet)
        {
            FocusOnUpdate();
            View.UpdateText = DirectMessagePrefix + " " + Tweet.Author.UserName + " ";
        }

        public void UpdateTextChanged() {

            if (ShouldShortenUrls) ShortenUrls();

            OnPaste = false;
        }

        bool ShouldShortenUrls { get { return 
            OnPaste || View.UpdateText.EndsWith(" ")
        ;}}

        void ShortenUrls() {
            View.UpdateText = UrlShrinker.Shorten(View.UpdateText);
        }

        public void UpdateTextPasted() {
            OnPaste = true;
        }
        
        public virtual PhotoWebService NewPhotoWebService { get { return Domain.ObjectFactory.NewPhotoWebService; } }

        public void UploadPhoto() {
            View.UpdateText = View.UpdateText.Insert(
                View.CursorPos, 
                NewPhotoWebService.Upload(View.RequestedPhoto)
            );
        }
    }
}