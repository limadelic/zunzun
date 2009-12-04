using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class ShorteningUrls : Spec {

        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        const string OriginalUrl = "http://www.twitter.com/zunzunapp";
        
        public ShorteningUrls() {
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        protected override void SetUpSteps() {

            When("entering an Update containing a url", () => {
                UpdateStatusView.UpdateText = OriginalUrl;
                UpdateStatusPresenter.UpdateTextChanged();
            });

            Then("the url should be shortened", () => 
                UpdateStatusView.UpdateText.Length
                    .ShouldBeLessThan(OriginalUrl.Length)
            );
        }
    }
}