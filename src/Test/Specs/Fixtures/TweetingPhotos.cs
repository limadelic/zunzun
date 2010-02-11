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
            
            Domain.Settings.PhotoService = "yfrog";
            Domain.Settings.PhotoService = "twitpic";

            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusView.UpdateText = "prefix  suffix";
            Given.That(UpdateStatusView).CursorPos.Is("prefix ".Length);
            
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        public bool Zunzun_should_allow_to_browse_for_the_photo() { return Verify.That(() => 
            Given.That(UpdateStatusView).RequestedPhoto.Is(Actors.Photo)
        );}

        public bool upload_it_into_a_website_that_stores_photos() { return Verify.That(() => 
            UpdateStatusPresenter.UploadPhoto()
        );}
        
        public bool and_paste_the_url_in_the_Tweet() { return Verify.That(() => 
            UpdateStatusView.UpdateText.ShouldContain("http://" + Domain.Settings.PhotoService)
        );}
        
        public bool The_url_should_not_be_shortened() { return Verify.That(() => {
            UpdateStatusView.UpdateText += " ";
            UpdateStatusPresenter.UpdateTextChanged();
            UpdateStatusView.UpdateText.ShouldContain("http://" + Domain.Settings.PhotoService);
        });}
        
        public bool and_should_be_pasted_at_cursor_position() { return Verify.That(() => {
            UpdateStatusView.UpdateText.ShouldStartWith("prefix ");
            UpdateStatusView.UpdateText.ShouldEndWith(" suffix ");
        });}
    }
}