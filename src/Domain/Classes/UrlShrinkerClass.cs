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
        
        string Url { get; set; }

        void ShortenIfUrl(string StatusUpdateToken) { Url = StatusUpdateToken;
            if (UrlShouldNotShorten) return;
            
            var ShortenedUrl = WebRequest.GetResponse(RequestToShortenUrl).Trim();
            
            if (ShortenedUrl.Length >= Url.Length) return;

            StatusUpdate = StatusUpdate.Replace(Url, ShortenedUrl);
        }

        bool UrlShouldNotShorten { get { return 
            ! Url.IsUrl()  
            || UrlIsAlreadyShortened
            || IsAPhotoUrl
        ;}}

        bool IsAPhotoUrl { get { return 
            Url.StartsWith("http://twitpic.com")
            || Url.StartsWith("http://yfrog.com")
        ;}}

        public static readonly Dictionary<string, string> Services = new Dictionary<string, string> {
            { "u.nu", "http://u.nu/unu-api-simple?url={0}"},
            { "is.gd", "http://is.gd/api.php?longurl={0}" },
            { "bit.ly", "http://bit.ly/api?url={0}" },
            { "tinyurl", "http://tinyurl.com/api-create.php?url={0}" },
        };

        bool UrlIsAlreadyShortened { get { return
            Url.StartsWith("http://" + Settings.UrlShrinker)
        ;}}

        string RequestToShortenUrl { get { return
            string.Format(Services[Settings.UrlShrinker], Url)
        ;}}
    }
}