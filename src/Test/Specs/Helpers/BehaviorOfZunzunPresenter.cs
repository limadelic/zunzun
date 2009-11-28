using System;
using System.Windows;
using FluentSpec;
using Zunzun.App.Presenters;

namespace Zunzun.Specs.Helpers {

    public class BehaviorOfZunzunPresenter : BehaviorOf<ZunzunPresenter> {
        
        protected Delegate GivenHandlerFor(EventHandler<RoutedEventArgs> Method) { 
            var Handler = new RoutedEventHandler(Method);
            Given.Handler(Method).Is(Handler);
            return Handler;
        }
    }
}