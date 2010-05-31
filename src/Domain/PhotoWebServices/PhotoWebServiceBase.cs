using System;
using System.IO;
using Zunzun.Utils;

namespace Zunzun.Domain.PhotoWebServices {

    public abstract class PhotoWebServiceBase : PhotoWebService {

        public WebRequest Request { get; set; }
        public WebRequestContent Content { get; set; }
        
        public abstract string RequestUrl { get; }
        
        public abstract string PhotoUrlFrom(string Response);

        protected string Photo;
        public abstract string PhotoNameKey { get; }
        public virtual byte[] PhotoData { get { return File.ReadAllBytes(Photo); } }
        public virtual string PhotoMetaData { get { return String.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", PhotoNameKey, PhotoFileName);} }
        public virtual string PhotoFileName { get { return Path.GetFileName(Photo); } }
        
        public string Upload(string Photo) {
            if (string.IsNullOrEmpty(Photo)) return "";
            this.Photo = Photo;

            SetUpContent();
            return PhotoUrlFrom(Response);
        }

        public virtual string Response { get { return Request.Post(RequestUrl, Content); } }

        public virtual void SetUpContent() {

            Content.AppendHeader();

            AddPhoto();
            AddCredentials();
            
            Content.AppendFooter();
        }
        
        public virtual void AddPhoto() {
            Content.AppendData(PhotoMetaData, "image/jpeg", PhotoData);
        }

        public virtual void AddCredentials() {
            Content.Append("username", Settings.UserName);
            Content.Append("password", Settings.Password);
            Content.Append("xml", "yes");
        }
    }
}