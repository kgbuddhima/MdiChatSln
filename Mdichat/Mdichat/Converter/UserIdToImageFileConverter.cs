using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mdichat.Converter
{
    public class UserIdToImageFileConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int contactId = (int) value;
            return string.Empty;
            //  return getContactUserImage(contactId);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private string getContactUserImage(int contactId)
        {
            var cachedContacts = Helpers.Settings.UserContacts;
            return cachedContacts == null ?
                string.Empty :
                cachedContacts.Find(u => u.ContactId == contactId)?.ImageFilePath.Replace("mdilarge","mdismall");
        }
    }
}
