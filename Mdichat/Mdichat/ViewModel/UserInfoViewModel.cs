using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mdichat.ViewModel
{
    public class UserInfoViewModel : INotifyPropertyChanged
    {
        public UserInfoViewModel()
        {

        }

        private Xamarin.Forms.ImageSource _image;
        public Xamarin.Forms.ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set
            {
                _mobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set
            {
                _nickName = value;
                OnPropertyChanged(nameof(NickName));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _suffix;
        public string Suffix
        {
            get => _suffix;
            set
            {
                _suffix = value;
                OnPropertyChanged(nameof(Suffix));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _organization;
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                OnPropertyChanged(nameof(Organization));
            }
        }

        private string _designation;
        public string Designation
        {
            get => _designation;
            set
            {
                _designation = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private string _address;
        public string Addresss
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Addresss));
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }

        private string _state;
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        private string _primarySchool;
        public string PrimarySchool
        {
            get => _primarySchool;
            set
            {
                _primarySchool = value;
                OnPropertyChanged(nameof(PrimarySchool));
            }
        }

        private string _highSchool;
        public string HighSchool
        {
            get => _highSchool;
            set
            {
                _highSchool = value;
                OnPropertyChanged(nameof(HighSchool));
            }
        }

        private string _collage;
        public string Collage
        {
            get => _collage;
            set
            {
                _collage = value;
                OnPropertyChanged(nameof(Collage));
            }
        }

        private string _graduatedSchool;
        public string GraduatedSchool
        {
            get => _graduatedSchool;
            set
            {
                _graduatedSchool = value;
                OnPropertyChanged(nameof(GraduatedSchool));
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private string _errorMsg;
        public string ErrorMessage
        {
            get => _errorMsg;
            set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand => new Command(async () => await SaveUserInfo());

        private async Task SaveUserInfo()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
