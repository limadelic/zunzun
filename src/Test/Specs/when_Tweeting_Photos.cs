using System;
using System.Text;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;
using Zunzun.Domain.PhotoWebServices;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    public class when_Tweeting_Photos {
    
        [TestClass]
        public class the_Presenter : BehaviorOf<UpdateStatusPresenter> {
        
            [TestMethod]
            public void should_upload_the_Photo_and_add_the_url_to_the_Tweet() {

                Given.View.UpdateText = string.Empty;
                Given.PhotoWebService.Upload("Photo").WillReturn("Url");
                
                When.UploadPhoto("Photo");
                
                Then.View.UpdateText.ShouldContain("Url");
            }

            [TestMethod]
            public void the_url_should_be_inserted_at_cursor_pos() {

                Given.View.UpdateText = "prefix  suffix";
                Given.View.CursorPos.Is("prefix ".Length);
                
                Given.PhotoWebService.Upload("Photo").WillReturn("Url");
                
                When.UploadPhoto("Photo");
                
                Then.View.UpdateText.ShouldBe("prefix Url suffix");
            }

        }
        
        [TestClass]
        public class with_TwitPic : BehaviorOf<TwitPic> {

            readonly byte[] ContentData = Actors.ContentData;
        
            [TestMethod]
            public void should_upload_the_photo_and_return_the_url() {

                Given.SendRequest().WillReturn("Url");
                When.Upload("Photo").ShouldBe("Url");
                Should.SetUpRequest();
            }
            
            [TestMethod]
            public void should_setup_boundary_and_content() {
                
                When.SetUpRequest();
                
                The.Boundary.ShouldNotBeNull();
                Should.SetUpContent();
            }

            [TestMethod]
            public void should_create_a_request() {
            
                Given.Boundary = Actors.Boundary;
                Given.ContentData.Is(ContentData);

                var Request = The.NewRequest;
                
                Request.PreAuthenticate.ShouldBeTrue();
                Request.AllowWriteStreamBuffering.ShouldBeTrue();
                Request.ContentType.ShouldContain(The.Boundary);
                Request.Method.ShouldBe("POST");
                Request.ContentLength.ShouldBe((long) ContentData.Length);
            }
            
            [TestMethod]
            public void should_read_photo_from_file() {

                var PhotoData = ContentData;
                var PhotoDataAsString = Encoding.GetEncoding(TwitPic.Encoding).GetString(PhotoData);

                Given.PhotoData.Is(PhotoData);
                Given.Content = new StringBuilder();
                
                When.AddPhoto();
                
                Then.Content.ToString().ShouldContain(PhotoDataAsString);
            }
            
            [TestMethod]
            public void should_include_credentials_in_request() {
            
                Given.Content = new StringBuilder();
                
                When.AddCredentials();
                
                Then.Content.ToString().ShouldContain("username");
                Then.Content.ToString().ShouldContain("password");
            }
            
            [TestMethod]
            public void should_add_header_and_footer() {
                
                Given.Boundary = Actors.Boundary;
                
                When.SetUpContent();
                
                Then.Content.ToString()
                    .ShouldContain(The.Header + Environment.NewLine);
                    
                Then.Content.ToString()
                    .ShouldContain(The.Footer + Environment.NewLine);
            }

            [TestMethod]
            public void should_extract_the_photo_url_from_response() {
                const string Response = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><rsp status=\"ok\"><statusid>7695597420</statusid><userid>81659009</userid><mediaid>xvjq8</mediaid><mediaurl>http://twitpic.com/xvjq8</mediaurl></rsp>";
                
                The.PhotoUrlFrom(Response).ShouldBe("http://twitpic.com/xvjq8");
            }
        }
        
        [TestClass]
        public class the_UrlShrinker : BehaviorOf<UrlShrinkerClass> {
        
            [TestMethod]    
            public void should_not_shorten_the_url() {
                const string Url = "http://twitpic.com/123456";

                Given.WebRequest.IgnoringArgs()
                    .GetResponse(null).WillReturn(Actors.ShortenedUrl);
                    
                When.Shorten(Url).ShouldBe(Url);
            }
        }
    }
}