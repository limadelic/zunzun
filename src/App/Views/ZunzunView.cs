using System;
using System.Windows;

namespace Zunzun.App.Views {

    public interface ZunzunView {
    
        void AddHandler(RoutedEvent Event, Delegate Handler);
    }
}