namespace Zunzun.Utils {

    public interface WebRequest {
    
        string Get(string Url);
        string Post(string Url, WebRequestContent Content);
    }
}