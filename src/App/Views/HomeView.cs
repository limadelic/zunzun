using System.Collections.Generic;
using Zunzun.Domain;

namespace Zunzun.App.Views {
    
    public interface HomeView {
    
        List<Tweet> Tweets { get; set; }
    }
}