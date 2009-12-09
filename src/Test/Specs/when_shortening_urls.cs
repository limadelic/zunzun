using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs {

    [TestClass]
    public class when_shortening_urls {
    
        const string OriginalUrl = "http://www.longurl.com/verylongpath";
        const string ShortenedUrl = "http://a.com";
        
        [TestClass]
        public class an_UpdateStatusPresenter : BehaviorOf<UpdateStatusPresenter>{
            
            [TestMethod]
            public void should_shorten_url_automatically() {
                
                Given.View.UpdateText = OriginalUrl;
                Given.UrlShrinker.Shorten(OriginalUrl).Is(ShortenedUrl);
                
                When.UpdateTextChanged();
                
                Then.View.UpdateText.ShouldBe(ShortenedUrl);
            }
        }
        
        [TestClass]
        public class an_UrlShrinker : BehaviorOf<UrlShrinkerClass> {
            
            const string UrlShortenRequest = "url request to url shorten provider";
            
            [TestInitialize]
            public void SetUp() {
                Given.RequestToShorten(OriginalUrl).Is(UrlShortenRequest);
                Given.WebRequest.GetResponse(UrlShortenRequest).Is(ShortenedUrl);
            }

            [TestMethod]
            public void should_request_a_shorter_url() {

                When.Shorten(OriginalUrl).ShouldBe(ShortenedUrl);
            }

            [TestMethod]
            public void should_request_to_shorten_only_urls() {
            
                const string OriginalStatusUpdate = 
                    " check out this link " + OriginalUrl + " cool right?";
                const string ShortenedStatusUpdate = 
                    " check out this link " + ShortenedUrl + " cool right?";
                
                When.Shorten(OriginalStatusUpdate)
                    .ShouldBe(ShortenedStatusUpdate);
            }
        }
    }
}