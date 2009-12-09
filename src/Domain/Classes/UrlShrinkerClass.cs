using Zunzun.Utils;
using Zunzun.Domain.Helpers;

namespace Zunzun.Domain.Classes {

    public class UrlShrinkerClass : UrlShrinker {
    
        readonly char[] BySpaces = new[] {' '};
        string StatusUpdate;
        
        public WebRequest WebRequest { get; set; }

        public string Shorten(string StatusUpdate) {
            this.StatusUpdate = StatusUpdate;

            StatusUpdate.Split(BySpaces)
                .ForEach(ShortenIfUrl);

            return this.StatusUpdate;
        }
        
        void ShortenIfUrl(string StatusUpdateToken) { 
            if (!StatusUpdateToken.IsUrl()) return;
            
            var ShortenedUrl = WebRequest.GetResponse(RequestToShorten(StatusUpdateToken));

            StatusUpdate = StatusUpdate.Replace(StatusUpdateToken, ShortenedUrl);
        }

        public virtual string RequestToShorten(string Url) { return
            "http://tinyurl.com/api-create.php?url=" + Url
        ;}
    }
}