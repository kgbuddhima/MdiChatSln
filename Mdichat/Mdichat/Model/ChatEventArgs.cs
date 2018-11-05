using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Model
{
    public class ChatEventArgs : EventArgs
    {
        public ChatEventArgs(string s, string groupId, string contactId, string userName, string chatId, string isfile)
        {
            message = s;
            GroupId = groupId;
            ContactId = contactId;
            UserName = userName;
            ChatId = chatId;

            bool hasFile = false;
            bool.TryParse(isfile,out hasFile);
            IsFile = hasFile;

        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string GroupId { get; set; }
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string ChatId { get; set; }
        public bool IsFile { get; set; }
    }
}
