using System;
using System.Text;

namespace Zunzun.Utils.Classes {

    public class WebRequestContentClass : WebRequestContent {
        
        public const string Encoding = "iso-8859-1";    

        public StringBuilder Content { get; set; }
        public virtual byte[] Data { get { return 
            System.Text.Encoding.GetEncoding(Encoding)
            .GetBytes(Content.ToString())
        ;}}
        public int Length { get { return Data.Length; } }

        public string Boundary { get; private set; } 
        public string Header { get { return string.Format("--{0}", Boundary); } }
        public string Footer { get { return string.Format("--{0}--", Boundary); } }

        public void AppendHeader() {
            Boundary = Guid.NewGuid().ToString();
            Content = new StringBuilder();
            Content.AppendLine(Header);
        }
        
        public void Append(string Name, string Value) {
            Append(Header, string.Format("Content-Disposition: form-data; name=\"{0}\"", Name), Value);
        }

        public void AppendData(string fileHeader, string ContentType, byte[] Value) {
            var Data = System.Text.Encoding.GetEncoding(Encoding).GetString(Value);
            Append(fileHeader, string.Format("Content-Type: {0}", ContentType), Data);
        }

        public void AppendFooter() {
            Content.AppendLine(Footer);
        }

        void Append(string LineHeader, string Name, string Value) {
            Content.AppendLine(LineHeader);
            Content.AppendLine(Name);
            Content.AppendLine();
            Content.AppendLine(Value);
        }
    }
}