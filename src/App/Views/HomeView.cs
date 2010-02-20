using System.Collections.ObjectModel;
using Zunzun.Domain;

namespace Zunzun.App.Views {
    
    public interface HomeView {
    
        ObservableCollection<Tweet> HomeTweets { get; set; }
        ObservableCollection<Tweet> ConvoTweets { get; set; }
        void MakeHomeVisible();
        void MakeConversationVisible();
    }
}