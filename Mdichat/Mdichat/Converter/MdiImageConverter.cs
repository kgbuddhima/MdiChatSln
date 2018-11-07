using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mdichat.Converter
{
    public class MdiImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var userImage = value as byte[];
            //if (userImage != null && userImage.Length == 0) return "defaultUser.png";

            if (value != null)
            {
                return ImageSource.FromStream(() => new MemoryStream(value as byte[]));
            }
            else
            {
                return ImageSource.FromStream(() => new MemoryStream(new byte[0]));
            }

            //userImage = await Task.Run(() =>
            //{
            //    return ImageSource.FromStream(() => new MemoryStream(value as byte[]));
            //});
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
