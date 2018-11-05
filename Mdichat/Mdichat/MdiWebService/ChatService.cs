using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using Newtonsoft.Json;

namespace MdiChat.MdiWebService
{
    public class ChatService: IChatService
    {
        private readonly IServiceBase _serviceBase;
        public ChatService()
        {
            _serviceBase = new ServiceBase();
        }
        public async Task<List<MdiContact>> SearchContacts(string searchStr)
        {
            try
            {
                var response = await _serviceBase.Get($"{Constants.SearchContactsUrl}?name={searchStr}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var contactlist = JsonConvert.DeserializeObject<List<MdiContact>>(result);
                    return contactlist;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public async Task<ChatResponse> SaveChatMessage(ChatMessage payload)
        {
            var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.SaveChatMessageUrl);
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChatResponse>(result);
        }

        public async Task<List<RecentChat>> GetRecentChats()
        {
            var response = await _serviceBase.Get($"{Constants.GetRecentChatsUrl}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RecentChat>>(result);
            }
            return null;
        }

        public async Task<bool> RegisterNotificationHub(NotificationHubRegistration payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.RegisterNotificationHubTokenUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<MdiContact>> GetUserContacts()
        {
            try
            {
                var response = await _serviceBase.Get($"{Constants.GetUserContacts}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var contactlist = JsonConvert.DeserializeObject<List<MdiContact>>(result);
                    return contactlist;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the collection of chat groups of an user
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChatGroup>> GetChatGroups()
        {
            try
            {
                var response = await _serviceBase.Get($"{Constants.GetChatGroups}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var contactlist = JsonConvert.DeserializeObject<List<ChatGroup>>(result);
                    return contactlist;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<RecentChat> GetChatMessageById(int chatId)
        {
          
            var response = await _serviceBase.Get($"{Constants.GetChatMessageById}?chatId={chatId}");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RecentChat>(result);
        }

        public async Task<List<RecentChat>> GetChatMessagesByGroupId(int groupId)
        {
            var response = await _serviceBase.Get($"{Constants.GetChatsByGroupId}?groupId={groupId}");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RecentChat>>(result);
        }
    }
}
