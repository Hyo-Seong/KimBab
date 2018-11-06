using KimBab.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KimBab.Converter
{
    public class PaymentWayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PaymentType)value == PaymentType.CARD)
            {
                return "카드결제";
            }
            return "현금결제";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}