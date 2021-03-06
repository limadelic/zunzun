﻿using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;

namespace Zunzun.App.Views.Xaml {

    public partial class Main : ZunzunView {
    
        readonly ZunzunPresenter Presenter;

        public Main() {
            Presenter = PresenterFactory.NewZunzunPresenter(this);
            InitializeComponent();
            RegisterEvents();
        }
        
        void RegisterEvents() {
            
            // needs to go inside the UpdateStatus control
            AddHandler(Reply.Event, new RoutedEventHandler(Update.OnReply));
            AddHandler(Retweet.Event, new RoutedEventHandler(Update.OnRetweet));
            AddHandler(DirectMessage.Event, new RoutedEventHandler(Update.OnDirectMessage));

            AddHandler(NewTweets.Event, new RoutedEventHandler(Home.OnNewTweets));
            AddHandler(Conversation.Event, new RoutedEventHandler(Home.ShowConversation));
            
            ToggleUpdate.Click += Update.OnToggleVisibility;
            ToggleSearch.Click += Search.OnToggleVisibility;
        }

        void OnClose(object Sender, RoutedEventArgs Args) {
            Close();
        }

        void OnDragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        	DragMove();
        }

        void OnGoHome(object Sender, RoutedEventArgs Args) { Show(Home); }

        public void Show(UIElement ContentControl) {
            ContentPlaceholder.Child = ContentControl;
        }

        public void RequestLogin() {
            var Login = new Settings { Owner = this };
            Login.ShowDialog();
        }

        void Load(object Sender, RoutedEventArgs Args) {
            Presenter.Load();
        }

        private void OnSettings(object sender, RoutedEventArgs e) {
            Presenter.ShowSettings();
        }
    }
}