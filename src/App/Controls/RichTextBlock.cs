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
            
            var TextBoxWithUrl = Sender as RichTextBlock;
            var Text = Args.NewValue.ToString();
            var Formatter = ObjectFactory.NewTextFormatter;

            TextBoxWithUrl.Inlines.Clear();
            TextBoxWithUrl.Inlines.AddRange(Formatter.TokensFrom(Text));
        }

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