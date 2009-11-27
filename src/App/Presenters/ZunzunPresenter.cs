using System;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Views;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class ZunzunPresenter {
    
        public UserService UserService { get; set; }
        public ZunzunView View { get; set; }
        
        public void RegisterEvents() {
            View.AddHandler(FollowUser.Event, Handler(OnFollowUser)); 
            View.AddHandler(UnfollowUser.Event, Handler(OnUnfollowUser)); 
        }

        public virtual Delegate Handler(EventHandler<RoutedEventArgs> Method) { return
            new RoutedEventHandler(Method)
        ;}

        public void OnFollowUser(object Sender, RoutedEventArgs Args) {
            var UserName = (Args as UserEvent.Args).UserName;
            UserService.Follow(UserName);
            UserChanged.With(UserName, View);
        }

        public void OnUnfollowUser(object Sender, RoutedEventArgs Args) {
            var UserName = (Args as UserEvent.Args).UserName;
            UserService.Unfollow(UserName);
            UserChanged.With(UserName, View);
        }
    }
}