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
        
        public bool The_Photo_should_be_uploaded_into_a_Photo_WebSite() { return this.Worked(() => 
            UpdateStatusPresenter.UploadPhoto("logo.png")
        );}
        
        public bool and_the_url_should_be_included_in_the_Tweet() {
            return UpdateStatusView.UpdateText.StartsWith("http://" + Domain.Settings.ImageUploader);
        }
    }
}