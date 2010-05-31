using System.Xml.Linq;

namespace Zunzun.Domain.PhotoWebServices {

    public class YFrog : PhotoWebServiceBase {
        
        public override string PhotoNameKey { get { return "fileupload"; }}

        public override string RequestUrl { get { return "http://www.imageshack.us/upload_api.php"; }}
        
        public override string PhotoUrlFrom(string Response) {
            const string Xmlns = "xmlns=\"http://ns.imageshack.us/imginfo/7/\"";

            var Xml =  XDocument.Parse(Response.Replace(Xmlns, ""));

            return Xml.Element("imginfo").Element("links")
                .Element("yfrog_link").Value;
        }
    }
}