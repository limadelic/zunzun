using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;

namespace Zunzun.Specs.Fixtures {

    public class ShorteningUrls : Spec {

        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        
        public ShorteningUrls() {
            Domain.Settings.UrlShrinker = "u.nu";
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        protected override void SetUpSteps() {

            Given("the url will be shorten with {0}", Service => 
                Domain.Settings.UrlShrinker = Service);

            When("making a {0} containing urls", PasteText);

            Then("the urls should be {0}", ShrinkedUpdate => 
                UpdateStatusView.UpdateText.ShouldBe(ShrinkedUpdate)
            );

            When("entering the status update {0}", Update => {
                UpdateStatusView.UpdateText = Update;
                UpdateStatusPresenter.UpdateTextChanged();
            });

            When("pasting {0} into the status update", PasteText);

            Then("the status update should be {0}", ShrinkedUpdate => 
                UpdateStatusView.UpdateText.ShouldBe(ShrinkedUpdate)
            );
        }

        void PasteText(string Update) {
            UpdateStatusView.UpdateText = Update;
            UpdateStatusPresenter.UpdateTextPasted();
            UpdateStatusPresenter.UpdateTextChanged();
        }

        string UrlShrinker;
        
        public void BackupUrlShrinker() {
            UrlShrinker = Domain.Settings.UrlShrinker;
        }

        public void RestoreUrlShrinker() {
            Domain.Settings.UrlShrinker = UrlShrinker;
        }
    }
}