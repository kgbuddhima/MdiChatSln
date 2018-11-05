using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.ViewModel
{
    public class ContactListVM: INotifyPropertyChanged
    {
        public ContactListVM()
        {
            _contactGroups = new ObservableCollection<ContactGroupModel>();
            ContactGroups = new ObservableCollection<ContactGroupModel>();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
