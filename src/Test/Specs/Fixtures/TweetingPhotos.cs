using FluentSpec;
using Zunzun.App.Presenters;
using Zunzun.App.Views;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class TweetingPhotos {
        
        readonly UpdateStatusPresenter UpdateStatusPresenter;
        readonly UpdateStatusView UpdateStatusView;
        
        public TweetingPhotos() {
            UpdateStatusView = Create.TestObjectFor<UpdateStatusView>();
            UpdateStatusPresenter = PresenterFactory.NewStatusPresenter(UpdateStatusView);
        }
        
        public bool The_photo_should_be_uploaded_into_a_website_that_stores_photos() { return this.Worked(() => 
            UpdateStatusPresenter.UploadPhoto(@"dotnet\images\screenshot.png")
        );}
        
        public bool and_the_url_should_be_included_in_the_Tweet() {
            return UpdateStatusView.UpdateText.StartsWith("http://" + Domain.Settings.ImageUploader);
        }
    }
}