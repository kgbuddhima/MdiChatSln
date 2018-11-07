using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Model
{
    public class AndroidNotificationMessage
    {
        public string message { get; set; }
        public string groupId { get; set; }
        public string contactId { get; set; }
        public string username { get; set; }
    }
}
