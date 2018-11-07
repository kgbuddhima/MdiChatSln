using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Mdichat.Model;

namespace Mdichat.ViewModel
{
    public class TeamInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int _groupId;
        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }

        string _groupType;
        public string GroupType
        {
            get => _groupType;
            set
            {
                _groupType = value; OnPropertyChanged(nameof(GroupType));
            }
        }

        string _groupName;
        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value; OnPropertyChanged(nameof(GroupName));
            }
        }

        string _firstname;
        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value; OnPropertyChanged(nameof(FirstName));
            }
        }

        string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value; OnPropertyChanged(nameof(LastName));
            }
        }

        DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        int _createdBy;
        public int CreatedBy
        {
            get => _createdBy;
            set
            {
                _createdBy = value; OnPropertyChanged(nameof(CreatedBy));
            }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        string patientdemographics;
        public string PatientDemographics
        {
            get { return patientdemographics; }
            set
            {
                patientdemographics = value;
                OnPropertyChanged(nameof(PatientDemographics));
            }
        }

        bool _soundOn;
        public bool ShoundOn
        {
            get { return _soundOn; }
            set
            {
                _soundOn = value;
                OnPropertyChanged(nameof(ShoundOn));
            }
        }

        bool _soundOff;
        public bool ShoundOff
        {
            get { return _soundOff; }
            set
            {
                _soundOff = !value;
                OnPropertyChanged(nameof(ShoundOff));
            }
        }

        bool _mobile;
        public bool Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        bool _waring;
        public bool Warning
        {
            get { return _waring; }
            set
            {
                _waring = value;
                OnPropertyChanged(nameof(Warning));
            }
        }

        bool _health;
        public bool Health
        {
            get { return _health; }
            set
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        ObservableCollection<ContactModel> _adminsCollection;
        public ObservableCollection<ContactModel> AdminsCollection
        {
            get { return _adminsCollection; }
            set
            {
                _adminsCollection = value;
                OnPropertyChanged(nameof(AdminsCollection));
            }
        }

        ObservableCollection<ContactModel> _membersCollection;
        public ObservableCollection<ContactModel> MembersCollection
        {
            get { return _membersCollection; }
            set
            {
                _membersCollection = value;
                OnPropertyChanged(nameof(MembersCollection));
            }
        }

        public TeamInfoViewModel()
        {

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
