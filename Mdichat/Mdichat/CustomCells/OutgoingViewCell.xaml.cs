using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.CustomCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OutgoingViewCell : ViewCell
    {
        public OutgoingViewCell()
        {
            InitializeComponent();
            var user = Helpers.Settings.User;
            if (user.UserImage != null && user.UserImage.Length > 0)
            {
                ImgUserImage.Source = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
            }                                  
        }
    }
}