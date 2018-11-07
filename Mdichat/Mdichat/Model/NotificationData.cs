using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Model
{
    public class NotificationData
    {
        public string ContentTitle { get; set; }
        public string Message { get; set; }
        public string GroupId { get; set; }
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string ChatId { get; set; }
        public bool IsFile { get; set; }
        public byte[] FileData { get; set; }
        public string FileExtension { get; set; }


    }
}
