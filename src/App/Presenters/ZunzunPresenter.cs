using System;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Views;
using Zunzun.App.Views.Xaml;
using Zunzun.Domain;

namespace Zunzun.App.Presenters {

    public class ZunzunPresenter {
    
        public UserService UserService { get; set; }
        public ZunzunView View { get; set; }
        
        public void RegisterEvents() {
            View.AddHandler(FollowUser.Event, Handler(OnFollowUser)); 
            View.AddHandler(UnfollowUser.Event, Handler(OnUnfollowUser)); 
            View.AddHandler(ShowUserHome.Event, Handler(OnShowUserHome)); 
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

        public void OnShowUserHome(object Sender, RoutedEventArgs Args) {
            var UserHome = NewUserHome;
            Show(UserHome, Sender, Args);
            View.AddHandler(UserChanged.Event, Handler(UserHome.OnUserChanged));
            View.Show(UserHome);
        }

        public virtual void Show(UserHome UserHome, object Sender, RoutedEventArgs Args) {
            UserHome.OnShowUserHome(Sender, Args);
        }

        public virtual UserHome NewUserHome { get { return new UserHome(); }}
    }
}