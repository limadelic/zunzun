using System;
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
            if (!IsUrl(StatusUpdateToken)) return;
            
            var ShortenedUrl = WebRequest.GetResponse(RequestToShorten(StatusUpdateToken));

            StatusUpdate = StatusUpdate.Replace(StatusUpdateToken, ShortenedUrl);
        }

        bool IsUrl(string Word) { return
            Uri.IsWellFormedUriString(Word, UriKind.Absolute)
            && Settings.AcceptedProtocols.Contains(UriScheme(Word))
        ;}
        
        string UriScheme(string Word) { return new Uri(Word).Scheme; }

        public virtual string RequestToShorten(string Url) { return
            "http://tinyurl.com/api-create.php?url=" + Url
        ;}
    }
}