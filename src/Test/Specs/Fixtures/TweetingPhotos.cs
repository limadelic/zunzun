using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;
using Given=FluentSpec.Given;

namespace Zunzun.Specs.Fixtures {

    public class TweetingPhotos {
        
        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        
        public TweetingPhotos() {
            
            Domain.Settings.ImageUploader = "twitpic";
            
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusView.UpdateText = "prefix  suffix";
            Given.That(UpdateStatusView).CursorPos.Is("prefix ".Length);
            
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        public bool The_photo_should_be_uploaded_into_a_website_that_stores_photos() { return Verify.That(() => 
            UpdateStatusPresenter.UploadPhoto(@"dotnet\images\screenshot.png")
        );}
        
        public bool and_the_url_should_be_included_in_the_Tweet() { return Verify.That(() => 
            UpdateStatusView.UpdateText.ShouldContain("http://" + Domain.Settings.ImageUploader)
        );}
        
        public bool The_url_should_not_be_shortened() { return Verify.That(() => {
            UpdateStatusView.UpdateText += " ";
            UpdateStatusPresenter.UpdateTextChanged();
            UpdateStatusView.UpdateText.ShouldContain("http://" + Domain.Settings.ImageUploader);
        });}
        
        public bool and_should_be_inserted_at_cursor_position() { return Verify.That(() => {
            UpdateStatusView.UpdateText.ShouldStartWith("prefix ");
            UpdateStatusView.UpdateText.ShouldEndWith(" suffix ");
        });}
    }
}