using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DouyuTV
{
    public class HotNumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                int hn = (int)value;
                if (hn < 10000)
                {
                    return hn.ToString();
                }
                else
                {
                    hn /= 10000;
                    return hn.ToString("0.0") + "万";
                }
            }
            catch { return "NaN"; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
