using System.Xml.Linq;

namespace Zunzun.Domain.PhotoWebServices {

    public class TwitPic : PhotoWebServiceBase {
        
        public override string PhotoNameKey { get { return "media"; }}

        public override string RequestUrl { get { return "http://twitpic.com/api/upload"; }}

        public override string PhotoUrlFrom(string Response) { return
            XDocument.Parse(Response)
            .Element("rsp").Element("mediaurl").Value
        ;}
    }
}