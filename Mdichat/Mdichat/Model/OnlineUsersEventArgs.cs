using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Model
{
   
    public class OnlineUsersEventArgs : EventArgs
    {
        public OnlineUsersEventArgs(List<string> s)
        {
            users = s;
        }
        private List<string> users;

        public List<string> Users
        {
            get => users;
            set => users = value;
        }
    }
}
