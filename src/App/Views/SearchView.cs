using System.Collections.Generic;
using Zunzun.Domain;

namespace Zunzun.App.Views {

    public interface SearchView {
    
        string SearchText { get; }
        List<Tweet> Tweets { get; set; }
        bool IsVisible { get; set; }
    }
}