using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_shortening_urls {
    
        const string OriginalUrl = "http://www.longurl.com/verylongpath";
        const string ShortenedUrl = Actors.ShortenedUrl;
        
        [TestClass]
        public class an_UpdateStatusPresenter : BehaviorOf<UpdateStatusPresenter>{
            
            [TestMethod]
            public void should_shorten_url_automatically_after_a_space() {
                
                Given.View.UpdateText = OriginalUrl + " ";
                Given.UrlShrinker.Shorten(OriginalUrl + " ").Is(ShortenedUrl + " ");
                
                When.UpdateTextChanged();
                
                Then.View.UpdateText.ShouldBe(ShortenedUrl + " ");
            }

            [TestMethod]
            public void should_not_shorten_urls_too_soon() {
                
                Given.View.UpdateText = OriginalUrl;
                
                When.UpdateTextChanged();
                
                Then.UrlShrinker.ShouldNot().Shorten(OriginalUrl);
                Then.View.UpdateText.ShouldBe(OriginalUrl);
            }

            [TestMethod]
            public void should_shorten_url_automatically_on_paste() {
                
                Given.View.UpdateText = OriginalUrl;
                Given.UrlShrinker.Shorten(OriginalUrl).Is(ShortenedUrl);
                
                When.UpdateTextPasted();
                When.UpdateTextChanged();
                
                Then.View.UpdateText.ShouldBe(ShortenedUrl);
            }
        }
        
        [TestClass]
        public class an_UrlShrinker : BehaviorOf<UrlShrinkerClass> {
            
            [TestInitialize]
            public void SetUp() {
                Given.WebRequest.IgnoringArgs()
                    .GetResponse(null).WillReturn(ShortenedUrl);
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
            
            [TestMethod]
            public void should_not_shorten_an_url_already_shortened() {
                
                When.Shorten(ShortenedUrl).ShouldBe(ShortenedUrl);
            }

            [TestMethod]
            public void should_not_shorten_a_short_url() {
                const string ShortUrl = "http://imdb.com";

                When.Shorten(ShortUrl).ShouldBe(ShortUrl);
            }
            
            [TestMethod]
            public void should_shorten_enclosed_urls() {
                
                When.Shorten("(" + OriginalUrl + ")")
                    .ShouldBe("(" + ShortenedUrl + ")");
                    
                When.Shorten("[" + OriginalUrl + "]")
                    .ShouldBe("[" + ShortenedUrl + "]");
                    
                When.Shorten("{" + OriginalUrl + "}")
                    .ShouldBe("{" + ShortenedUrl + "}");
            }
        }
    }
}