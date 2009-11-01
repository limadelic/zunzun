using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Zunzun.App.Controls {
    public class BindableRichTextBox : RichTextBox {
        // See MS TextBox._isInsideTextContentChange (via Reflector)

        bool _isInsideTextContentChange;


        public BindableRichTextBox() { TextChanged += _onTextChanged; }


        void _onTextChanged(object sender, TextChangedEventArgs e) {
            if (_isInsideTextContentChange) return;


            _isInsideTextContentChange = true;

            Text = _getText();

            _isInsideTextContentChange = false;
        }

        #region DEPENDENCY PROPERTY: Text

        public string Text {
            [DebuggerStepThrough]
            get { return (string) GetValue(TextProperty); }

            [DebuggerStepThrough]
            set { SetValue(TextProperty, value); }
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string),
            typeof (BindableRichTextBox),
            new FrameworkPropertyMetadata("",
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                _onTextPropertyChanged, _coerceText, true, UpdateSourceTrigger.LostFocus));


        static void _onTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var o = (BindableRichTextBox) d;

            var v = (string) e.NewValue;


            if (o._isInsideTextContentChange) return;


            o._isInsideTextContentChange = true;

            o._setText(v);

            o._isInsideTextContentChange = false;


            o._clearUndo();
        }


        static object _coerceText(DependencyObject d, object value) { return value ?? ""; }

        #endregion

        string _getText() { return new TextRange(Document.ContentStart, Document.ContentEnd).Text; }

        void _setText(string text) {
//                        new TextRange(Document.ContentStart, Document.ContentEnd).Text = text;
//            text =
//                "<Paragraph> <Hyperlink NavigateUri=\"http://google.com\" RequestNavigate=\"OpenUrl\">google</Hyperlink></Paragraph>";
//            
            var para = new Paragraph();
            var hyperlink = new Hyperlink(new Run("google")) {NavigateUri = new Uri("http://google.com")};
            hyperlink.Click += OpenUrl;
            para.Inlines.Add(hyperlink);
            Document.Blocks.Add(para);
////            using (var xamlMemoryStream = new MemoryStream(Encoding.ASCII.GetBytes(text))) {
////                var parser = new ParserContext();
////                parser.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
////                parser.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
////                Document = new FlowDocument();
////                //                var section = XamlReader.Load(xamlMemoryStream, parser) as Section;
////                //                Document.Blocks.Add(section);
////                Document.Blocks.Add(XamlReader.Load(xamlMemoryStream, parser) as Block);
////            }
        }

        void OpenUrl(object Sender, RoutedEventArgs e) { 
            Process.Start(new ProcessStartInfo("www.google.com"));

            e.Handled = true;
        }

        void _clearUndo() {
            var limit = UndoLimit;
            UndoLimit = 0;
            UndoLimit = limit;
        }
    }
}