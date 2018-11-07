using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdichat.MdiWebService.DTO;
using Mdichat.Model;

namespace Mdichat.MdiWebService
{
    public interface IChatService
    {
        Task<List<MdiContact>> SearchContacts(string searchStr);
        Task<List<MdiContact>> GetUserContacts();
        Task<ChatResponse> SaveChatMessage(ChatMessage payload);
        Task<List<RecentChat>> GetRecentChats();
        Task<bool> RegisterNotificationHub(NotificationHubRegistration payload);
        /// <summary>
        /// Get the collection of chat groups of an user
        /// </summary>
        /// <returns></returns>
        Task<List<ChatGroup>> GetChatGroups();

        Task<RecentChat> GetChatMessageById(int chatId);
        Task<List<RecentChat>> GetChatMessagesByGroupId(int groupId);

    }
}
