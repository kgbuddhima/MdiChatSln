using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Mdichat.ViewModel
{
    public class NewGroupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NewGroupViewModel()
        {

        }

        int _groupId;
        string _groupType;
        string _groupName;
        string _firstname;
        string _lastName;
        DateTime _dateOfBirth;
        int _createdBy;

        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }
        public string GroupType
        {
            get => _groupType;
            set
            {
                _groupType = value; OnPropertyChanged(nameof(GroupType));
            }
        }
        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value; OnPropertyChanged(nameof(GroupName));
            }
        }
        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value; OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value; OnPropertyChanged(nameof(LastName));
            }
        }
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public int CreatedBy
        {
            get => _createdBy;
            set
            {
                _createdBy = value; OnPropertyChanged(nameof(CreatedBy));
            }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
