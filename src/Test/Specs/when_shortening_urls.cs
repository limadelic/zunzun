using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;

namespace Zunzun.Specs {

    [TestClass]
    public class when_shortening_urls {
    
        const string OriginalUrl = "htp://www.longurl.com/verylongpath";
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
            
            [TestMethod]
            public void should_request_a_shorter_url_with_tinyurl() {
                const string UrlShortenRequest = "http://tinyurl.com/api-create.php?url={" + OriginalUrl + "}";
                
                Given.WebRequest.GetResponse(UrlShortenRequest).Is(ShortenedUrl);
                When.Shorten(OriginalUrl).ShouldBe(ShortenedUrl);
            }
        }
    }
}