using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.View.ValueConverter
{
    class CardColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int position = (int)value;
            if ((position % 4)==0)
            {
                return Color.FromRgb(0,16,218);
            }
            else if ((position % 3) == 0)
            {
                return Color.FromRgb(253, 193, 11);
            }
            else if ((position % 2) == 0)
            {
                return Color.FromRgb(234, 37, 46);
            }
            else
            {
                return Color.FromRgb(126, 194, 73);
            } 

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
