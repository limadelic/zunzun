using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Zunzun.App.Converters;
using Zunzun.App.Events;
using Zunzun.App.Presenters;
using Zunzun.Domain;

namespace Zunzun.App.Views.Xaml {

    public partial class UpdateStatus : UpdateStatusView {
    
        UpdateStatusPresenter Presenter { get; set; }

        public UpdateStatus() {
            Presenter = PresenterFactory.NewStatusPresenter(this);
            InitializeComponent();
        }

        public new bool IsVisible { 
            get { return base.IsVisible; } 
            set { this.IsVisibleIf(value); } 
        }

        public string RequestedPhoto { get {

            var OpenFileDialog = new CommonOpenFileDialog {
                Filters = { new CommonFileDialogFilter("Images", "jpg,jpeg,png,gif") }
            };
            
            return OpenFileDialog.ShowDialog() == CommonFileDialogResult.OK?
                OpenFileDialog.FileName : null;
                
            #region might b needed for xp

//            var OpenFileDialog = new OpenFileDialog {
//                Filter = "Images (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif"
//            };
//
//            return OpenFileDialog.ShowDialog().Value ? OpenFileDialog.FileName : "";
 
	        #endregion        
	    }}

        public void FocusOnUpdate() {
            TweetContent.Focus();
        }

        public string UpdateText {
            get { return TweetContent.Text; } 
            set {
				TweetContent.Text = value;
                TweetContent.SelectionStart = value.Length;
            }
        }

        public int CursorPos { get { return TweetContent.CaretIndex; } }

        void OnSend(object Sender, RoutedEventArgs Args) {
        	Presenter.Update();
        }

        public void OnToggleVisibility(object Sender, RoutedEventArgs Args) {
            Presenter.ToggleUpdateVisibility();
        }

        public void OnReply(object Sender, RoutedEventArgs Args) {
            ExecuteTweetEvent(Presenter.ReplyTo, Args);
        }

        public void OnRetweet(object Sender, RoutedEventArgs Args){
            ExecuteTweetEvent(Presenter.Retweet, Args);
        }

        public void OnDirectMessage(object Sender, RoutedEventArgs Args) {
            ExecuteTweetEvent(Presenter.DirectMessage, Args);
        }

        void ExecuteTweetEvent(Action<Tweet> Event, RoutedEventArgs Args) {
            var Tweet = (Args as TweetEvent.Args).Tweet;
            Event(Tweet);
        }

        void OnTextChanged(object Sender, TextChangedEventArgs Args) {
            Presenter.UpdateTextChanged();
        }

        void OnTextPasted(object Sender, DataObjectPastingEventArgs Args) {
            Presenter.UpdateTextPasted();
        }

        private void OnUploadPhoto(object sender, RoutedEventArgs e) {
            Presenter.UploadPhoto();
        }
    }
}