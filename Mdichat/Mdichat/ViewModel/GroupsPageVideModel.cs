using Mdichat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.ViewModel
{
    public class GroupsPageVideModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private ObservableCollection<ContactGroupModel> _contactGroups;
        public ObservableCollection<ContactGroupModel> ContactGroups
        {
            get
            {
                return _contactGroups;
            }
            set
            {
                _contactGroups = value;
                OnPropertyChanged(nameof(ContactGroups));
            }
        }

        private NewGroupViewModel _newGroupModel;
        public NewGroupViewModel NewGroupModel
        {
            get
            {
                return _newGroupModel;
            }
            set
            {
                _newGroupModel = value;
                OnPropertyChanged(nameof(NewGroupModel));
            }
        }

        public GroupsPageVideModel()
        {
            _contactGroups = new ObservableCollection<ContactGroupModel>();
            NewGroupModel = new NewGroupViewModel();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
