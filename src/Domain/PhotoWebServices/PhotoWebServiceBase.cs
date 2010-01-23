using System;
using System.IO;
using System.Text;
using Zunzun.Utils;

namespace Zunzun.Domain.PhotoWebServices {

    public abstract class PhotoWebServiceBase : PhotoWebService {

        public const string Encoding = "iso-8859-1";    

        protected string Photo;
        public virtual byte[] PhotoData { get { return File.ReadAllBytes(Photo); } }
        public virtual string PhotoFileName { get { return Path.GetFileName(Photo); } }
        
        public string Upload(string Photo) {
            if (string.IsNullOrEmpty(Photo)) return "";
            this.Photo = Photo;

            SetUpContent();
            return PhotoUrlFrom(Response);
        }

        public virtual string Response { get { return NewRequest.Post(RequestUrl, ContentData, Boundary); } }

        public abstract string RequestUrl { get; }
        
        public abstract string PhotoUrlFrom(string Response);

        public virtual WebRequest NewRequest { get { return Utils.ObjectFactory.NewWebRequest; } }

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









        public StringBuilder Content { get; set; }
        public virtual byte[] ContentData { get { return 
            System.Text.Encoding.GetEncoding(Encoding)
            .GetBytes(Content.ToString())
        ;}}
        
        readonly string Boundary = Guid.NewGuid().ToString();
        public string Header { get { return string.Format("--{0}", Boundary); } }
        public string Footer { get { return string.Format("--{0}--", Boundary); } }

        void AppendContent(string header, string Field, string Value) {
            Append(header, string.Format("Content-Disposition: form-data; name=\"{0}\"", Field), Value);
        }

        void AppendData(string fileHeader, string Field, string Value) {
            Append(fileHeader, string.Format("Content-Type: {0}", Field), Value);
        }
        
        void Append(string Header, string Field, string Value) {
            Content.AppendLine(Header);
            Content.AppendLine(Field);
            Content.AppendLine();
            Content.AppendLine(Value);
        }

    }
}