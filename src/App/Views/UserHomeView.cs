using System.Collections.ObjectModel;
using Zunzun.Domain;

namespace Zunzun.App.Views {
    
    public interface UserHomeView {

        User User { get; set; }
        ObservableCollection<Tweet> Tweets { get; set; }
    }
}