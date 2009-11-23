using System;
using System.Globalization;
using System.Windows.Data;

namespace Zunzun.App.Converters {

    public class DateToString : IValueConverter {
    
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDateTime(value).ToString(parameter.ToString());
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}