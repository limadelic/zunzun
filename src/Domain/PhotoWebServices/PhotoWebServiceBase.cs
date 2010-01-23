using System;
using System.IO;
using System.Net;
using System.Text;

namespace Zunzun.Domain.PhotoWebServices {

    public abstract class PhotoWebServiceBase : PhotoWebService {

        public const string Encoding = "iso-8859-1";    

        protected string Photo;
        public virtual byte[] PhotoData { get { return File.ReadAllBytes(Photo); } }
        public virtual string PhotoFileName { get { return Path.GetFileName(Photo); } }
        
        public string Header { get { return string.Format("--{0}", Boundary); } }
        public string Footer { get { return string.Format("--{0}--", Boundary); } }
        
        public StringBuilder Content { get; set; }

        public string Upload(string Photo) {
            if (string.IsNullOrEmpty(Photo)) return "";
            this.Photo = Photo;

            SetUpRequest();
            return SendRequest();
        }

        public string Boundary { get; set; }
        
        public virtual void SetUpRequest() {
            Boundary = Guid.NewGuid().ToString();
            SetUpContent();
        }
        
        public virtual byte[] ContentData { get { return 
            System.Text.Encoding.GetEncoding(Encoding)
            .GetBytes(Content.ToString())
        ;}}
        
        public abstract string RequestUrl { get; }
        
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

        public abstract string PhotoUrlFrom(string Response);

        public virtual void SetUpContent() {

            Content = new StringBuilder();

            Content.AppendLine(Header);

            AddPhoto();
            AddCredentials();
            
            Content.AppendLine(Footer);
        }

        public abstract string PhotoNameKey { get; }
        
        public virtual void AddPhoto() {
            var Header = String.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", PhotoNameKey, PhotoFileName);
            var Data = System.Text.Encoding.GetEncoding(Encoding).GetString(PhotoData);

            AppendData(Header, "image/jpeg", Data);
        }

        public virtual void AddCredentials() {
            AppendContent(Header, "username", Settings.UserName);
            AppendContent(Header, "password", Settings.Password);
            AppendContent(Header, "xml", "yes");
        }
        
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
    }
}