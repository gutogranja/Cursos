using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Armazenador.Apresentation.Wpf.Views.Converters
{
    public class StringToNumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                //return Regex.Replace(value?.ToString(), @"[^0-9]+", "");
                return Regex.Replace(value.ToString(), @"[^0-9]+", "");
            return null;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = Regex.Replace(value.ToString(), @"[^0-9]+", "");
            return string.IsNullOrWhiteSpace(t) ? "0" : t;





        }
    }
}
