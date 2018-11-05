using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Helpers;
using System.ComponentModel;

namespace MdiChat.ViewModel
{
    public class PermisionCodeVM : INotifyPropertyChanged
    {
        public bool IsPermissionCodeOn
        {
            get
            {
                return Settings.PermissionCodeOn;
            }
            set
            {
                if (Settings.PermissionCodeOn == value)
                    return;
                Settings.PermissionCodeOn = value;
                OnPropertyChanged("IsPermissionCodeOn");
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
