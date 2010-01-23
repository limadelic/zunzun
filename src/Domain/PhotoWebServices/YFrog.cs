using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Zunzun.Domain.PhotoWebServices {

    public class YFrog : PhotoWebService {
        
        public const string Encoding = "iso-8859-1";
        
        string Photo;

        public string Boundary { get; set; }
        
        public string Header { get { return string.Format("--{0}", Boundary); } }
        public string Footer { get { return string.Format("--{0}--", Boundary); } }
        
        public StringBuilder Content { get; set; }
        
        public virtual byte[] ContentData { get { return 
            System.Text.Encoding.GetEncoding(Encoding)
            .GetBytes(Content.ToString())
        ;}}

        public string Upload(string Photo) {
            if (string.IsNullOrEmpty(Photo)) return "";
            this.Photo = Photo;

            SetUpRequest();
            return SendRequest();
        }

        public virtual void SetUpRequest() {
            Boundary = Guid.NewGuid().ToString();
            SetUpContent();
        }
        
        public virtual void SetUpContent() {

            Content = new StringBuilder();

            Content.AppendLine(Header);

            AddPhoto();
            AppendContent(Header, "xml", "yes");
            AddCredentials();

            Content.AppendLine(Footer);
        }

        public virtual void AddCredentials() {
            AppendContent(Header, "username", Settings.UserName);
            AppendContent(Header, "password", Settings.Password);
        }

        public virtual void AddPhoto() {
            var Header = String.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", "fileupload", PhotoFileName);
            var Data = System.Text.Encoding.GetEncoding(Encoding).GetString(PhotoData);

            AppendData(Header, "image/jpeg", Data);
        }

        public virtual byte[] PhotoData { get { return File.ReadAllBytes(Photo); } }
        
        public virtual string PhotoFileName { get { return Path.GetFileName(Photo); } }
        
        public HttpWebRequest NewRequest { get {
        
            var Request = (HttpWebRequest) WebRequest.Create(RequestUrl);
            
            Request.PreAuthenticate = true;
            Request.AllowWriteStreamBuffering = true;
            Request.ContentType = string.Format("multipart/form-data; boundary={0}", Boundary);
            Request.Method = "POST";
            Request.ContentLength = ContentData.Length;
            
            return Request;
        }}

        public virtual string SendRequest() {
            var Request = NewRequest;

            using (var RequestStream = Request.GetRequestStream()) {
                RequestStream.Write(ContentData, 0, ContentData.Length);

                using (var Response = (HttpWebResponse) Request.GetResponse()) 
                    using (var Reader = new StreamReader(Response.GetResponseStream())) 
                        return PhotoUrlFrom(Reader.ReadToEnd());
            }
        }

        const string RequestUrl = "http://www.imageshack.us/upload_api.php";

        void AppendContent(string header, string Field, string Value) {
            Append(header, String.Format("Content-Disposition: form-data; name=\"{0}\"", Field), Value);
        }

        void AppendData(string fileHeader, string Field, string Value) {
            Append(fileHeader, String.Format("Content-Type: {0}", Field), Value);
        }
        
        void Append(string Header, string Field, string Value) {
            Content.AppendLine(Header);
            Content.AppendLine(Field);
            Content.AppendLine();
            Content.AppendLine(Value);
        }

        public string PhotoUrlFrom(string Response) {
            const string Xmlns = "xmlns=\"http://ns.imageshack.us/imginfo/7/\"";

            var Xml =  XDocument.Parse(Response.Replace(Xmlns, ""));

            return Xml.Element("imginfo").Element("links")
                .Element("yfrog_link").Value;
        }
    }
}