using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.MdiWebService.DTO
{
    public class ChatMessage
    {
        public ChatMessage()
        {
            this.Members = new List<ChatReciever>();
        }

        public int GroupId { get; set; }
        public List<ChatReciever> Members { get; set; }
        public string Message { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public bool IsFile { get; set; }
        public DateTime MessageDateTime { get; set; }
        public int ChatId { get; set; }
        public string imageFilePath { get; set; }
        
    }

    public class ChatReciever
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
