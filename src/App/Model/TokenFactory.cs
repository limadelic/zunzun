using System;
using System.Windows.Documents;
using System.Windows.Media;
using Zunzun.App.Controls;

namespace Zunzun.App.Model {

    public static class TokenFactory {
        
        public static Inline NewLinkToUserHome(string Mention) {
            var Url = Domain.Settings.TwitterUrl + Mention;
            var ToolTip = "View " + Mention + "'s recent tweets";

            return NewLink(Mention, Url, ToolTip);
        }
        
        public static Inline NewLink(string Url) {
            return NewLink(Url, Url, Url);
        }

        static Inline NewLink(string Text, string Url, string ToolTip) {
            var Link = new Hyperlink(new Run(Text)) {
                NavigateUri = new Uri(Url),
                ToolTip = ToolTip,
            };
            
            Link.Click += RichTextBlock.OpenUrl;
            Link.Foreground = Link.TryFindResource(Settings.ContentBrush) as Brush;

            return Link;
        }
        
        public static Inline NewLiteral(string Text) { return new Run(Text); }
    }
}