using Zunzun.Utils;

namespace Zunzun.Domain.Classes {

    public class UrlShrinkerClass : UrlShrinker {
    
        readonly char[] ByDelimiters = new[] {' ', '[', ']', '{', '}', '(', ')' };
        string StatusUpdate;
        
        public WebRequest WebRequest { get; set; }

        public string Shorten(string StatusUpdate) {
            this.StatusUpdate = StatusUpdate;

            StatusUpdate.Split(ByDelimiters)
                .ForEach(ShortenIfUrl);

            return this.StatusUpdate;
        }
        
        void ShortenIfUrl(string StatusUpdateToken) { 
            if (!StatusUpdateToken.IsUrl()  
                || AlreadyShortened(StatusUpdateToken)) return;
            
            var ShortenedUrl = WebRequest.GetResponse(RequestToShorten(StatusUpdateToken));
            
            if (ShortenedUrl.Length >= StatusUpdateToken.Length) return;

            StatusUpdate = StatusUpdate.Replace(StatusUpdateToken, ShortenedUrl);
        }

        bool AlreadyShortened(string Url) { return
            Url.StartsWith("http://tinyurl.com")
        ;}

        public virtual string RequestToShorten(string Url) { return
            "http://tinyurl.com/api-create.php?url=" + Url
        ;}
    }
}