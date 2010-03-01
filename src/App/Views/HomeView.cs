using System.Collections.Generic;
using Zunzun.Domain;

namespace Zunzun.App.Views {
    
    public interface HomeView {
    
//        void Insert(List<Tweet> tweets);
        void Show(List<Tweet> tweets);
    }
}