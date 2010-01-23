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
            public void should_request_and_upload_the_Photo_and_add_the_url_to_the_Tweet_at_cursor_pos() {

                Given.View.UpdateText = "prefix  suffix";
                Given.View.CursorPos.Is("prefix ".Length);
                Given.View.RequestedPhoto.Is("Photo");
                
                Given.PhotoWebService.Upload("Photo").WillReturn("Url");
                
                When.UploadPhoto();
                
                Then.View.UpdateText.ShouldBe("prefix Url suffix");
            }

        }
        
        [TestClass]
        public class with_TwitPic : BehaviorOf<TwitPic> {

            readonly byte[] ContentData = Actors.ContentData;
            
            [TestMethod]
            public void should_return_empty_if_the_Photo_is_not_provided() {
                
                When.Upload(null).ShouldBe("");
            }

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
        public class with_YFrog : BehaviorOf<YFrog> {
            
            [TestMethod]
            public void should_extract_the_photo_url_from_response() {
                const string Response = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><imginfo xmlns=\"http://ns.imageshack.us/imginfo/7/\" version=\"7\" timestamp=\"1264215150\"><rating><ratings>0</ratings><avg>0.0</avg></rating><files server=\"130\" bucket=\"3483\"><image size=\"305599\" content-type=\"image/png\">screenshotzji.png</image><thumb size=\"3926\" content-type=\"image/jpeg\">screenshotzji.th.png</thumb></files><resolution><width>313</width><height>623</height></resolution><class>r</class><visibility>no</visibility><uploader><ip>67.14.242.57</ip></uploader><links><image_link>http://img130.imageshack.us/img130/3483/screenshotzji.png</image_link><image_html>&lt;a href=&quot;http://img130.imageshack.us/my.php?image=screenshotzji.png&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://img130.imageshack.us/img130/3483/screenshotzji.png&quot; alt=&quot;Free Image Hosting at www.ImageShack.us&quot; border=&quot;0&quot;/&gt;&lt;/a&gt;</image_html><image_bb>[URL=http://img130.imageshack.us/my.php?image=screenshotzji.png][IMG]http://img130.imageshack.us/img130/3483/screenshotzji.png[/IMG][/URL]</image_bb><image_bb2>[url=http://img130.imageshack.us/my.php?image=screenshotzji.png][img=http://img130.imageshack.us/img130/3483/screenshotzji.png][/url]</image_bb2><thumb_link>http://img130.imageshack.us/img130/3483/screenshotzji.th.png</thumb_link><thumb_html>&lt;a href=&quot;http://img130.imageshack.us/my.php?image=screenshotzji.png&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://img130.imageshack.us/img130/3483/screenshotzji.th.png&quot; alt=&quot;Free Image Hosting at www.ImageShack.us&quot; border=&quot;0&quot;/&gt;&lt;/a&gt;</thumb_html><thumb_bb>[URL=http://img130.imageshack.us/my.php?image=screenshotzji.png][IMG]http://img130.imageshack.us/img130/3483/screenshotzji.th.png[/IMG][/URL]</thumb_bb><thumb_bb2>[url=http://img130.imageshack.us/my.php?image=screenshotzji.png][img=http://img130.imageshack.us/img130/3483/screenshotzji.th.png][/url]</thumb_bb2><yfrog_link>http://yfrog.com/3mscreenshotzjip</yfrog_link><yfrog_thumb>http://yfrog.com/3mscreenshotzjip.th.jpg</yfrog_thumb><ad_link>http://img130.imageshack.us/my.php?image=screenshotzji.png</ad_link><done_page>http://img130.imageshack.us/content.php?page=done&amp;l=img130/3483/screenshotzji.png</done_page></links></imginfo>";
                
                The.PhotoUrlFrom(Response).ShouldBe("http://yfrog.com/3mscreenshotzjip");
            }
        }
        
        [TestClass]
        public class the_UrlShrinker : BehaviorOf<UrlShrinkerClass> {
        
            [TestMethod]    
            public void should_not_shorten_the_url() {
                const string Url = "http://twitpic.com/123456 http://yfrog.com/123456";

                Given.WebRequest.IgnoringArgs()
                    .GetResponse(null).WillReturn(Actors.ShortenedUrl);
                    
                When.Shorten(Url).ShouldBe(Url);
            }
        }
    }
}