using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using SQLite;
using Xamarin.Forms;

namespace MdiChat.Persistence
{
    public class ChatService
    {
        private SQLiteAsyncConnection _connection = null;

        public ChatService()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<RecentChat>();

        }

        public async Task UpdateChatMessageImagePath(Message message)
        {
            var messageFromDb = await _connection.Table<RecentChat>()
                .Where(x =>  x.Id == message.Id).FirstOrDefaultAsync();
            if (messageFromDb == null) return;

            messageFromDb.ImageFilePath = message.ImageFilePath;

           await _connection.UpdateAsync(messageFromDb);
        }

        public async Task SaveChatMessageToLocalDb(RecentChat chat)
        {
           await _connection.InsertAsync(chat);
        }

        public async Task<bool> IsGroupIdExists(int groupId)
        {
            try
            {
                var result = await _connection.Table<RecentChat>().Where(c => c.GroupId == groupId).FirstOrDefaultAsync();

                if (result == null) return false;

                return true;

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
