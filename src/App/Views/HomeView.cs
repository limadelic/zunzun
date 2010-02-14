using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zunzun.Domain;

namespace Zunzun.App.Views {
    
    public interface HomeView {
    
        ObservableCollection<Tweet> Tweets { get; set; }
        void OnNewTweets(List<Tweet> Tweets);
    }
}