using System.IO;
using System.Text;

namespace Zunzun.Utils.Classes {

    public class WebRequestClass : WebRequest {

        public string GetResponse(string Url) { 
            var Request = System.Net.WebRequest.Create(Url);
            
            using (var Response = Request.GetResponse().GetResponseStream()) 
                return new StreamReader(Response, Encoding.ASCII).ReadToEnd();
        }
    }
}