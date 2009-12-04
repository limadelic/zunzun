using System;
using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using ObjectFactory = Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures {

    public class ShowConversation : Spec {

        private UpdateStatusPresenter Presenter;
        private string origContent;

        public ShowConversation()
        {
            Presenter = PresenterFactory.NewStatusPresenter(Create.TestObjectFor<UpdateStatusView>());
        }

        protected override void SetUpSteps() {

            Given("I say {0}", Something =>
            {
                Presenter.Update(ObjectFactory.NewTweet(Something));
                origContent = Something;
            } );

            And("I reply {0} to the original tweet", Something =>
            {
                var origId = GetOriginalId();
            });

            When("I look at the Conversation", Pending);

            Then("it should say {0}", Something => Pending());
        }

        private long GetOriginalId()
        {
            return 0;
        }
    }
}