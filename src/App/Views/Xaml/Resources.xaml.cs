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

        void OnShowUserHome(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.ShowUserHome.Of(Tweet.Author.UserName, Sender);
        }

        void OnConversation(object Sender, RoutedEventArgs Args) {
            var Tweet = (Sender as FrameworkElement).DataContext as Tweet;
            Events.Conversation.To(Tweet, Sender);
        }
    }
}