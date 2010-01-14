using System;
using System.Text;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain;
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
                Should.SetUpRequest();
            }
            
            [TestMethod]
            public void should_setup_a_boundary() {
                
                When.SetUpRequest();
                The.Boundary.ShouldNotBeNull();
            }

            [TestMethod]
            public void should_create_a_request() {
            
                Given.Boundary = Guid.NewGuid().ToString();

                var Request = The.NewRequest;
                
                Request.PreAuthenticate.ShouldBeTrue();
                Request.AllowWriteStreamBuffering.ShouldBeTrue();
                Request.ContentType.ShouldContain(The.Boundary);
                Request.Method.ShouldBe("POST");
            }
            
            [TestMethod]
            public void should_read_photo_from_file() {

                var PhotoData = new byte[] {1, 2, 42};
                var PhotoDataAsString = Encoding.GetEncoding(TwitPic.Encoding).GetString(PhotoData);

                Given.PhotoData.Is(PhotoData);
                Given.Content = new StringBuilder();
                
                When.AddPhoto();
                
                The.Content.ToString().ShouldContain(PhotoDataAsString);
            }
            
            [TestMethod]
            public void should_include_credentials_in_request() {
            
                Given.Content = new StringBuilder();
                
                When.AddCredentials();
                
                The.Content.ToString().ShouldContain("username");
                The.Content.ToString().ShouldContain("password");
            }
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