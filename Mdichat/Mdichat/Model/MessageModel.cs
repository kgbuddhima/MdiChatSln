using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Model
{
    public class MessageModel
    {
        public int ID { get; set; }

        public int RecieverID { get; set; }

        public string RecieverName { get; set; }

        public string ImageMessage { get; set; }

        public int SenderID { get; set; }

        public string SenderName { get; set; }

        public string MessageDsc { get; set; }

        public string ImageUser { get; set; }

        public bool isOwnerP { get; set; }

        public DateTime TimeReceived { get; set; }
    }
}
