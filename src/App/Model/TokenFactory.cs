using System;
using System.Windows.Documents;
using Zunzun.App.Controls;

namespace Zunzun.App.Model {

    public static class TokenFactory {
        
        public static Inline NewLinkToUserHome(string Mention) {
            var Url = "http://twitter.com/" + Mention.Remove(0, 1);

            return NewLink(Mention, Url);
        }
        
        public static Inline NewLink(string Url) {
            return NewLink(Url, Url);
        }

        static Inline NewLink(string Text, string Url) {
            var Link = new Hyperlink(new Run(Text)) {
                NavigateUri = new Uri(Url),
                ToolTip = Text
            };
            
            Link.Click += RichTextBlock.OpenUrl;

            return Link;
        }

        public static Inline NewLiteral(string Text) { return new Run(Text); }
    }
}