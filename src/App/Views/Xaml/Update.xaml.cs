﻿using System;
using System.Windows;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class Update : StatusView {
    
        StatusPresenter Presenter { get; set; }

        public Update() {
            Presenter = PresenterFactory.NewStatusPresenter(this);
            InitializeComponent();
        }

        public new bool IsVisible { 
            get { return base.IsVisible; } 
            set { Visibility = value ? Visibility.Visible : Visibility.Collapsed; } 
        }

        public void FocusOnUpdate() {
            TweetContent.Focus();
        }

        public string UpdateText {
            get { return TweetContent.Text; } 
            set
            {
				TweetContent.Text = value;
                TweetContent.SelectionStart = value.Length;
            }
        }

        void OnSend(object Sender, RoutedEventArgs Args) {
        	Presenter.Update();
        }

        public void OnToggleVisibility(object Sender, RoutedEventArgs Args) {
            Presenter.ToggleUpdateVisibility();
        }

        public void OnReply(object Sender, RoutedEventArgs Args) {
            ExecuteTweetEvent(Presenter.ReplyTo, Args);
        }

        public void OnRetweet(object sender, RoutedEventArgs Args){
            ExecuteTweetEvent(Presenter.Retweet, Args);
        }

        public void OnDirectMessage(object sender, RoutedEventArgs Args) {
            ExecuteTweetEvent(Presenter.DirectMessage, Args);
        }

        void ExecuteTweetEvent(Action<Tweet> Action, RoutedEventArgs Args) {
            var Tweet = (Args as TweetEvent.Args).Tweet;
            Action(Tweet);
        }
    }
}