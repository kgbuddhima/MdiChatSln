using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdichat.ViewModel;
using Xamarin.Forms;

namespace Mdichat.Model
{
    public class ContactModel : BaseViewModel
    {
        public int ContactID { get; set; }

        public string ContactNumber { get; set; }

        public string ContactName { get; set; }

        public string Contact_FirstName { get; set; }

        public string Contact_LastName { get; set; }

        public string Contact_DisplayName { get; set; }

        public string Contact_EmailId { get; set; }

        public string FirstLetter { get; set; }

        public string Image { get; set; }

        private string _imageFilePath;

        public string ImageFilePath
        {
            get => this._imageFilePath;
            set
            {
                _imageFilePath = value;
                RaisePropertyChanged(() => ImageFilePath);
            }
        }

        public string Owner { get; set; }

        public string Contact_Picture { get; set; }
        public int GroupId { get; set; }
        public ParamObject Parameters { get; set; }
        /* {
             get
             {
                 return Device.OnPlatform("default_human_pic@2x", "ic_action_default_human_pic.png", "Images/default_human_pic.png");
             }
         }*/
    }

    public class ContactGroupModel : ObservableCollection<ContactModel>
    {
        public string ContactTitle { get; set; }

        public string ContactShortTitle { get; set; }

        public string Image { get; set; }

    }

    public class ParamObject
    {
        public int GroupId { get; set; }
        public int ContactId { get; set; }
        public string UserName { get; set; }
    }
}
