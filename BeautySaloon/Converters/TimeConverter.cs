using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautySaloon.Converters
{
    internal class TimeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan)value;
            int cas = (int)time.TotalSeconds / 3600;
            int day = (int)cas / 24;


            int min = (int)time.TotalSeconds / 60 - cas * 60;
            cas = cas - day * 24;
            string res = day.ToString() + " день " + cas.ToString().PadLeft(2, '0') + " часа " + min.ToString().PadLeft(2, '0') + " минут";
            if (day == 0)
            {
                res = cas.ToString().PadLeft(2, '0') + " часа " + min.ToString().PadLeft(2, '0') + " минут";
                return res;
            }
            if (day == 1)
            {
                res = day.ToString() + " день " + cas.ToString().PadLeft(2, '0') + " часа " + min.ToString().PadLeft(2, '0') + " минут";
                return res;
            }

            if (cas == 0)
            {

                res = min.ToString().PadLeft(2, '0') + " минут";
                return res;
            }
            else
            {
                return res;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
