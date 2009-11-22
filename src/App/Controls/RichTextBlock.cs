using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Zunzun.App.Controls {

    public class RichTextBlock : TextBlock {
    
        static RichTextBlock() {
            TextProperty.OverrideMetadata(typeof(RichTextBlock), 
                new FrameworkPropertyMetadata(OnTextPropertyChanged));
        }

        static void OnTextPropertyChanged(DependencyObject Sender, DependencyPropertyChangedEventArgs Args) {
            if (Args.NewValue.ToString() == Args.OldValue.ToString()) return;
            
            ReformatText(Sender as RichTextBlock, Args.NewValue.ToString()); 
        }

        static void ReformatText(RichTextBlock RichTextBlock, string Text) {
            if (RichTextBlock.IsReformatting) return;

            var Formatter = ObjectFactory.NewTextFormatter;

            try { RichTextBlock.StartReformatting();

                RichTextBlock.Inlines.Clear();
                RichTextBlock.Inlines.AddRange(Formatter.TokensFrom(Text));

            } finally { RichTextBlock.StopReformatting(); }
        }

        public bool IsReformatting { get; private set; }
        void StartReformatting() { IsReformatting = true; }
        void StopReformatting() { IsReformatting = false; }

        public static void OpenUrl(object Sender, RoutedEventArgs E) {
            var Link = Sender as Hyperlink;
             
            Process.Start(new ProcessStartInfo(Link.NavigateUri.AbsoluteUri));
        }

        public static void ShowUserHome(object Sender, RoutedEventArgs E) {
            var Link = Sender as Hyperlink;
            var UserName = (Link.Inlines.FirstInline as Run).Text;
            
            Events.ShowUserHome.Of(UserName, Sender);
        }
    }
}