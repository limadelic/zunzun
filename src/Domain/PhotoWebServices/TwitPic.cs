using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Zunzun.Domain.PhotoWebServices {

    public class TwitPic : PhotoWebService {
        
        string Photo;

        public string Upload(string Photo) {
            this.Photo = Photo;
            PrepareRequest();
            return SendRequest();
        }

        public virtual string SendRequest() { return string.Empty; }
        public virtual void PrepareRequest() {
            
        }
        
        byte[] PhotoData { get { return File.ReadAllBytes(Photo); } }
        
        string PhotoFileName { get { return Path.GetFileName(Photo); } }        

        const string RequestUrl = "http://twitpic.com/api/upload";

        public string UploadPhoto() {

            // Documentation: http://www.twitpic.com/api.do
            var boundary = Guid.NewGuid().ToString();
            const string encoding = "iso-8859-1";

            var request = (HttpWebRequest) WebRequest.Create(RequestUrl);
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            request.Method = "POST";

            var header = string.Format("--{0}", boundary);
            var footer = string.Format("--{0}--", boundary);

            var contents = new StringBuilder();
            contents.AppendLine(header);

            const string fileContentType = "image/jpeg"; 
            var fileHeader = String.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", "media",
                PhotoFileName);
            var fileData = Encoding.GetEncoding(encoding).GetString(PhotoData);

            AppendData(contents, fileHeader, fileContentType, fileData);
            AppendContent(contents, header, "username", Settings.UserName);
            AppendContent(contents, header, "password", Settings.Password);

            contents.AppendLine(footer);

            var bytes = Encoding.GetEncoding(encoding).GetBytes(contents.ToString());
            request.ContentLength = bytes.Length;

            using (var requestStream = request.GetRequestStream()) {
                requestStream.Write(bytes, 0, bytes.Length);

                using (var response = (HttpWebResponse) request.GetResponse()) 
                    using (var reader = new StreamReader(response.GetResponseStream())) 
                        return PhotoUrl(reader.ReadToEnd());
            }
        }

        void AppendContent(StringBuilder contents, string header, string Field, string Value) {
            Append(contents, header, String.Format("Content-Disposition: form-data; name=\"{0}\"", Field), Value);
        }

        void AppendData(StringBuilder contents, string fileHeader, string Field, string Value) {
            Append(contents, fileHeader, String.Format("Content-Type: {0}", Field), Value);
        }
        
        void Append(StringBuilder contents, string Header, string Field, string Value) {
            contents.AppendLine(Header);
            contents.AppendLine(Field);
            contents.AppendLine();
            contents.AppendLine(Value);
        }

        public string PhotoUrl(string Response) { return
            XDocument.Parse(Response)
            .Element("rsp").Element("mediaurl").Value
        ;}

    }
}