using System.Linq;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Domain;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs.Fixtures {

    public class ShowConversation : Spec {

        private readonly UpdateStatusPresenter StatusPresenter;
        private readonly HomePresenter ConvoPresenter;
        private readonly HomeView HomeView;
        private Tweet origTweet;
        private string origContent;

        public ShowConversation()
        {
            HomeView = Create.TestObjectFor<HomeView>();
            StatusPresenter = PresenterFactory.NewStatusPresenter(Create.TestObjectFor<UpdateStatusView>());
            ConvoPresenter = PresenterFactory.NewHomePresenter(HomeView);
            ConvoPresenter.Load();
        }

        protected override void SetUpSteps() {

            Given("I say {0}", Something => StatusPresenter.Update(new TweetClass { Content = origContent = Something }));

            And("I reply with {0}", Something =>
            {
                origTweet = GetOriginalTweet();
                StatusPresenter.ReplyTo(origTweet);
                StatusPresenter.View.UpdateText += Something;
                StatusPresenter.Update();
            });

            When("I look at the Conversation", () => ConvoPresenter.ShowConversation(origTweet));

            Then("it should say {0}", Something => ConvoPresenter.Tweets.Contains(new TweetClass {Content = Something }));
        }

        private Tweet GetOriginalTweet() { return new TweetServiceClass().Tweets.Where(x => x.Content == origContent).First(); }
    }

}