using Dimebrain.TweetSharp.Model;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures
{
    public class ReplyToTweet : Spec 
    {
        private Tweet Tweet;
        private readonly StatusView View;
        private readonly StatusPresenter Presenter;
        
        public ReplyToTweet()
        {
            View = Create.TestObjectFor<StatusView>();
            Presenter = PresenterFactory.NewStatusPresenter(View);
        }

        protected override void SetUpSteps()
        {
            Given[@"a tweet by user ""User Name"""] = () =>
            {
                var status = new TwitterStatus { User = new TwitterUser{ Name = Expected["User Name"] }};
                Tweet = ObjectFactory.NewTweet(status);
            };

            When["I reply to the Tweet"] = () =>
            {
                Presenter.ReplyTo(Tweet);
            };

            Then[@"Update Text contains ""Reply Prefix"""] = () =>
            {
                View.UpdateText.ShouldContain( Expected["Reply Prefix"] );
            };
        }
    }
}