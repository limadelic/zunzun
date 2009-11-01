using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Zunzun.App.Converters {

    public class SourceToString : IValueConverter {
    
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var Pattern = new Regex(">(?<name>.*?)</a>");
            var Match = Pattern.Match(value.ToString());
            
            return "via " + (Match.Success ? Match.Groups["name"].Value : value);
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}