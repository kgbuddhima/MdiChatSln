using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Model
{
    public class MessageInfoModel
    {
        public int ContactId { get; set; }
        public int GroupId { get; set; }
        public string Username { get; set; }
        public int ChatId { get; set; }
    }
}
