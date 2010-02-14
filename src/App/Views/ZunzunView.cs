using System;
using System.Collections.Generic;
using System.Windows;
using Zunzun.Domain;

namespace Zunzun.App.Views {

    public interface ZunzunView {
    
        void AddHandler(RoutedEvent Event, Delegate Handler);
        void Show(UIElement ContentControl);
        void RequestLogin();
        void Close();
        void Show(List<Tweet> Tweets);
    }
}