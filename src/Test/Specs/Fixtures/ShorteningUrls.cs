using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class ShorteningUrls : Spec {

        readonly StatusPresenter StatusPresenter;
        readonly StatusView StatusView;
        const string OriginalUrl = "http://www.twitter.com/zunzunapp";
        
        public ShorteningUrls() {
            StatusView = Create.TestObjectFor<StatusView>();
            StatusPresenter = PresenterFactory.NewStatusPresenter(StatusView);
        }
        
        protected override void SetUpSteps() {

            When("entering an Update containing a url", () => {
                StatusView.UpdateText = OriginalUrl;
                StatusPresenter.UpdateTextChanged();
            });

            Then("the url should be shortened", () => 
                StatusView.UpdateText.Length
                    .ShouldBeLessThan(OriginalUrl.Length)
            );
        }
    }
}