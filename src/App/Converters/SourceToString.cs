using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Data;

namespace Zunzun.App.Converters {

    public class SourceToString : IValueConverter {
    
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var Value = HttpUtility.HtmlDecode(value.ToString());
            
            if (value.ToString() == "web") return "via web";
            
            var Pattern = new Regex(">(?<name>.*?)</a>");
            var Match = Pattern.Match(Value);
            
            return "via " + (Match.Success ? Match.Groups["name"].Value : "unknown");
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}