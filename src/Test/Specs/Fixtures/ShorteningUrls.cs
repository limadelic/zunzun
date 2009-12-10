using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class ShorteningUrls : Spec {

        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        
        public ShorteningUrls() {
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        protected override void SetUpSteps() {

            When("making a {0} containing urls", Update => {
                UpdateStatusView.UpdateText = Update + " ";
                UpdateStatusPresenter.UpdateTextChanged();
            });

            Then("the urls should be {0}", ShrinkedUpdate => 
                UpdateStatusView.UpdateText.ShouldBe(ShrinkedUpdate + " ")
            );
        }
    }
}