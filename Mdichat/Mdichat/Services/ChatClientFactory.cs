using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Services
{
    public static class ChatClientFactory

    {
        private static ChatClient _chatClient;
        public static ChatClient GetChatClient()
        {
            return _chatClient ?? (_chatClient = new ChatClient());
        }
    }
}
