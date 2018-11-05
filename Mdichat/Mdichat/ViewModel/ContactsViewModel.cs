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
    public class ContactsViewModel : INotifyPropertyChanged
    {
        private List<ContactModel> _contactsCollection;
        public List<ContactModel> ContactsCollection {
            get
            {
                return _contactsCollection;
            }
            set
            {
                _contactsCollection = value;
                OnPropertyChanged(nameof(ContactsCollection));
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
