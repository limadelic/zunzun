using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;

namespace Zunzun.Specs {

    [TestClass]
    public class when_shortening_urls {
    
        [TestClass]
        public class an_UpdateStatusPresenter : BehaviorOf<UpdateStatusPresenter>{
            
            [TestMethod]
            public void should_shorten_url_automatically() {
                const string OriginalUrl = "htp://www.longurl.com/verylongpath";
                const string ShortenedUrl = "http://a.com";
                
                Given.View.UpdateText = OriginalUrl;
                Given.UrlShrinker.Shorten(OriginalUrl).Is(ShortenedUrl);
                
                When.UpdateTextChanged();
                
                Then.View.UpdateText.ShouldBe(ShortenedUrl);
            }
        }
        
    }
}