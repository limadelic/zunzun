using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Resources {
        
        void OnReply(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.Reply.To(Tweet, Sender);
        }
        
        void OnRetweet(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.Retweet.To(Tweet, Sender);
        }
        
        void OnDirectMessage(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.DirectMessage.To(Tweet, Sender);
        }
    }
}