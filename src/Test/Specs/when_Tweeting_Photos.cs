using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.PhotoWebServices;
using Zunzun.Specs.Helpers;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

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
        
        [TestClass]
        public class with_TwitPic : BehaviorOf<TwitPic> {
            
            [TestMethod]
            public void should_upload_the_photo_and_return_the_url() {

                Given.SendRequest().WillReturn("Url");
                When.Upload("Photo").ShouldBe("Url");
                Should.PrepareRequest();
            }
            
//            [TestMethod]
//            public void 
        }

        [TestClass]
        public class SpikeUploadPhoto {

            readonly TwitPic TwitPic = (TwitPic) ObjectFactory.NewTwitPic;
            
            [TestInitialize]
            public void SetUp() {
                Domain.Settings.UserName = "kinobot";
                Domain.Settings.Password = "kashmir";
            }

            [TestMethod]
            [Ignore]
            public void using_twitpic() {

                TwitPic.Upload(@"a:\test.png").ShouldNotBeEmpty();
            }
            
            [TestMethod]
            [Ignore]
            public void should_extract_the_photo_url_from_response() {
                const string response = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><rsp status=\"ok\"><statusid>7695597420</statusid><userid>81659009</userid><mediaid>xvjq8</mediaid><mediaurl>http://twitpic.com/xvjq8</mediaurl></rsp>";
                TwitPic.PhotoUrl(response).ShouldBe("http://twitpic.com/xvjq8");
            }
        }
    }
}