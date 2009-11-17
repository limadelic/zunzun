using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Resources {
        
        void OnReply(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.Reply.To(Tweet, Sender);
        }
    }
}