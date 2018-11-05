using MdiChat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using MdiChat.MdiWebService.DTO;
using System.Windows.Input;
using MdiChat.Persistence;
using SQLite;
using Xamarin.Forms;
using MdiChat.Services;

namespace MdiChat.ViewModel
{
    public class ChatStreamViewModel : BaseViewModel
    {
        private List<RecentChat> _recentChatList = new List<RecentChat>();
        private List<Message> _messageList = new List<Message>();
        string outgoingText = string.Empty;
        private int _page = 0;
        private int _pageSize = 10;
        private SQLiteAsyncConnection _connection = null;

        public string OutGoingText
        {
            get { return outgoingText; }
            set { this.OutGoingText = value; }
        }

        public ICommand SendCommand { get; set; }


        public ICommand LocationCommand { get; set; }


        public int ContactId { get; set; }

        public int GroupId { get; set; }

        public int SenderId { get; set; }
        public ObservableCollection<Message> Messages { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler DidCompleteInitialLoad;
        public event EventHandler DidInsertNextPage;
        public event EventHandler WillInsertNextPage;
        private ObservableCollection<MdiContact> _groupMembersCollection;
        private List<MdiContact> _cachedContacts;
        private MdiChat.Persistence.ChatService _localChatService;

        public ChatStreamViewModel()
        {
            Messages = new ObservableCollection<Message>();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<RecentChat>();
            _cachedContacts = Helpers.Settings.UserContacts;
            var currentUser = Helpers.Settings.User;
            this.SenderId = currentUser.MdiUserId;
            _localChatService = new ChatService();
        }



        public ObservableCollection<MdiContact> GroupMembersCollection
        {
            get => _groupMembersCollection;
            set
            {
                _groupMembersCollection = value;
                RaisePropertyChanged();
            }
        }

        public async void Load(int chatId = 0)
        {
            IsLoading = true;
            try
            {

                if (chatId > 0)
                {
                    var isGroupExistInlocal = await _localChatService.IsGroupIdExists(GroupId);
                    if (isGroupExistInlocal)
                    {
                        // get chat by Id
                        var result = await App.ServiceManager.GetChatMessageById(chatId);
                        // save image 
                        if (result.IsFile)
                        {
                            var imageFileName = await DependencyService.Get<IFileService>()
                             .SaveByteArrayAsImageFile(result.FileData, $"{result.SenderId}{result.MessageDate.Ticks}");
                            result.ImageFilePath = imageFileName;

                        }

                        await _localChatService.SaveChatMessageToLocalDb(result);
                    }
                }

                var newMessages = await LoadRecentMessage();


                var previousCount = Messages.Count;
                int index = 0;

                //Notify that we're going to change messages, so that UI can store current list position and later restore it
                WillInsertNextPage?.Invoke(this, EventArgs.Empty);
                foreach (var message in newMessages)
                {
                    Messages.Insert(index++, message);
                }

                if (previousCount == 0)
                {
                    //Notify the UI, that this is first load, and the chat page should scroll to bottom to display the last message
                    DidCompleteInitialLoad?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    //Notify the UI that we completed inserting new items, and that chat page should try to restore previous scroll position
                    DidInsertNextPage?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            IsLoading = false;
        }


        public async Task<List<Message>> LoadRecentMessage()
        {
            IsLoading = true;

            _recentChatList = await GetRecentChats();
            _messageList = await MapRecentChatToMessageType(_recentChatList);

            return await GetItems(_page++, _pageSize);

        }

        public void SaveChatMessageToLocalDb(RecentChat chat)
        {
            _connection.InsertAsync(chat);
        }
        public void SaveChatMessagesToLocalDb(List<RecentChat> chat)
        {
            _connection.InsertAllAsync(chat);
        }

        private async Task<List<RecentChat>> GetRecentChats()
        {
            try
            {
                // TODO only load chats under respective groupId
                List<RecentChat> result;

                result = await _connection.Table<RecentChat>()
                    .Where(x => x.GroupId == this.GroupId).ToListAsync();

                if (result != null && result.Count > 0) return result;

                // load from remote
                // result = await App.ServiceManager.GetRecentChats(null);
                result = await App.ServiceManager.GetChatMessagesByGroupId(GroupId);

                // Convert Images and save images as files
                result = await UpdateChatImages(result);

                // Save to local
                await _connection.InsertAllAsync(result);


                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<List<Message>> MapRecentChatToMessageType(List<RecentChat> recentChats)
        {
            try
            {

                return await Task.Run(() =>
                {

                    return recentChats?
                        .OrderByDescending(o => o.MessageDate)
                        ?.Select(s =>
                            new Message
                            {
                                Text = s.LastMessage,
                                IsIncoming = s.IsInComming,
                                MessageDateTime = s.MessageDate,
                                SenderId = s.SenderId,
                                FileData = s.FileData,
                                IsFileIsImage = s.IsFile,
                                Id = s.Id,
                                ImageFilePath = s.ImageFilePath,
                                UserImagePath = GetContactUserImage(s.SenderId)


                            })?.ToList();
                });

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<List<Message>> GetItems(int pageIndex, int pageSize)
        {
            if (_messageList.Count == 0) return new List<Message>();

            if (_messageList.Skip(pageIndex * pageSize) == null) return new List<Message>();

            //HACK remove this :)
           await Task.Delay(0);

            return _messageList.Skip(pageIndex * pageSize)?.Take(pageSize)
                ?.OrderBy(m => m.MessageDateTime)
                .ToList();
        }
        private string GetContactUserImage(int contactId)
        {

            return _cachedContacts == null ?
                string.Empty :
                _cachedContacts.Find(u => u.ContactId == contactId)?.ImageFilePath.Replace("mdilarge", "mdismall");
        }

        private async Task<List<RecentChat>> GetChatMessagesByGroupId(int groupId)
        {
           return await App.ServiceManager.GetChatMessagesByGroupId(groupId);
        }

        private async Task<List<RecentChat>> UpdateChatImages(List<RecentChat> chatMessages)
        {
            foreach (var vmMessage in chatMessages)
            {
                if (vmMessage.IsFile && vmMessage.FileData != null
                     && string.IsNullOrEmpty(vmMessage.ImageFilePath))
                {

                    if (vmMessage.FileData.Count() == 0) continue;

                    var fileName = await DependencyService.Get<IFileService>()
                        .SaveByteArrayAsImageFile(vmMessage.FileData, $"{vmMessage.SenderId}{vmMessage.MessageDate.Ticks}");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        vmMessage.ImageFilePath = fileName;
                    });

                }
            }

            return chatMessages;
        }
    }
}
