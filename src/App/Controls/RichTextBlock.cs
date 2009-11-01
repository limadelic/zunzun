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

        static void OnTextPropertyChanged(DependencyObject D, DependencyPropertyChangedEventArgs E) {
            if (E.NewValue.ToString() == E.OldValue.ToString()) return;
            
            var TextBoxWithUrl = D as RichTextBlock;
            var Text = E.NewValue.ToString();
            var Formatter = ObjectFactory.NewTextFormatter;

            TextBoxWithUrl.Inlines.Clear();
            TextBoxWithUrl.Inlines.AddRange(Formatter.TokensFrom(Text));
        }

        public static void OpenUrl(object Sender, RoutedEventArgs E) {
            var Link = Sender as Hyperlink;
             
            Process.Start(new ProcessStartInfo(Link.NavigateUri.AbsoluteUri));

            E.Handled = true;
        }
    }
}