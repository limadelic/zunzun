using System.Collections.Generic;
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
            
            var ShortenedUrl = WebRequest.GetResponse(RequestToShorten(StatusUpdateToken)).Trim();
            
            if (ShortenedUrl.Length >= StatusUpdateToken.Length) return;

            StatusUpdate = StatusUpdate.Replace(StatusUpdateToken, ShortenedUrl);
        }

        public static readonly Dictionary<string, string> Services = new Dictionary<string, string> {
            { "u.nu", "http://u.nu/unu-api-simple?url={0}"},
            { "is.gd", "http://is.gd/api.php?longurl={0}" },
            { "bit.ly", "http://bit.ly/api?url={0}" },
            { "tinyurl", "http://tinyurl.com/api-create.php?url={0}" },
        };

        bool AlreadyShortened(string Url) { return
            Url.StartsWith("http://" + Settings.UrlShrinker)
        ;}

        public virtual string RequestToShorten(string Url) { return
            string.Format(Services[Settings.UrlShrinker], Url)
        ;}
    }
}