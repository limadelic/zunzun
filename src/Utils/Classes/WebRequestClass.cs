using System.IO;
using System.Net;
using System.Text;

namespace Zunzun.Utils.Classes {

    public class WebRequestClass : WebRequest {

        public string Get(string Url) { 
            var Request = System.Net.WebRequest.Create(Url);
            
            using (var Response = Request.GetResponse().GetResponseStream()) 
                return new StreamReader(Response, Encoding.ASCII).ReadToEnd();
        }

        public HttpWebRequest NewRequest(string Url, byte[] Content, string Boundary) { 
        
            var Request = (HttpWebRequest) System.Net.WebRequest.Create(Url);
            
            Request.PreAuthenticate = true;
            Request.AllowWriteStreamBuffering = true;
            Request.ContentType = string.Format("multipart/form-data; boundary={0}", Boundary);
            Request.Method = "POST";
            Request.ContentLength = Content.Length;
            
            return Request;
        }
    
        public string Post(string Url, byte[] Content, string Boundary) {
            var Request = NewRequest(Url, Content, Boundary);

            using (var RequestStream = Request.GetRequestStream()) {
                RequestStream.Write(Content, 0, Content.Length);

                using (var Response = Request.GetResponse()) 
                    using (var Reader = new StreamReader(Response.GetResponseStream())) 
                        return Reader.ReadToEnd();
            }
        }
    }
}