using Zunzun.Utils;

namespace Zunzun.Domain.Classes {

    public class UrlShrinkerClass : UrlShrinker {

        string Url;
        public WebRequest WebRequest { get; set; }

        public string Shorten(string Url) {
            this.Url = Url;
            
            return WebRequest.GetResponse(UrlShortenRequest);
        }

        public virtual string UrlShortenRequest { get { return
            "http://tinyurl.com/api-create.php?url={" + Url + "}"
        ;}}
    }
}