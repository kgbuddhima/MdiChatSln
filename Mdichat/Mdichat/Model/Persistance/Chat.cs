using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Model.Persistance
{
    public class Chat
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsInComming { get; set; }
        public int SenderId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public bool IsFile { get; set; }
    }
}
