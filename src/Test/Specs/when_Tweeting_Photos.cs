using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    public class when_Tweeting_Photos {
        
        [TestClass]
        public class the_Presenter : BehaviorOf<UpdateStatusPresenter> {
        
            [TestMethod]
            public void should_upload_the_Photo_and_add_the_url_to_the_Tweet() {

                Given.PhotoWebService.Upload("Photo").WillReturn("Url");
                When.UploadPhoto("Photo");
                The.View.UpdateText.ShouldContain("Url");
            }
        }
    }
}