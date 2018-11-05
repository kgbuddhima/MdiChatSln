using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Model;
using MvvmHelpers;
using System.Windows.Input;
using MdiChat.MdiWebService.DTO;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MdiChat.ViewModel
{
    public class MainChatViewModel : INotifyPropertyChanged
    {
        private List<RecentChat> _recentChatList = new List<RecentChat>();
        private List<Message> _messageList = new List<Message>();

        public ObservableRangeCollection<Message> Messages { get; }

        public event EventHandler<Message> RaiseMessageLoaded;

        string outgoingText = string.Empty;

        public string OutGoingText
        {
            get { return outgoingText; }
            set { this.OutGoingText = value; }
        }

        public ICommand SendCommand { get; set; }


        public ICommand LocationCommand { get; set; }
        public ICommand LoadMore
        {
            get;
            set;
        }

        public int ContactId { get; set; }

        public int GroupId { get; set; }

        private bool _isLoadingMore = true;

        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }

        private int _page = 1;
        private int _pageSize = 10;


        private ObservableCollection<MdiContact> _groupMembersCollection;
        public ObservableCollection<MdiContact> GroupMembersCollection
        {
            get => _groupMembersCollection;
            set
            {
                _groupMembersCollection = value;
                OnPropertyChanged(nameof(GroupMembersCollection));
            }
        }

        public MainChatViewModel()
        {
            LoadMore = new Command(async() =>
            {
                var lastItem = Messages[0];
                //_page++;
                //await LoadMeesagesByPage(_page, _pageSize);
                //IsLoadingMore = false;
                lastItem.Text = lastItem.Text + DateTime.Now.ToString();
                Messages.Insert(0, lastItem);
                //OnRaiseMessageLoaded(lastItem);
            });

            Messages = new ObservableRangeCollection<Message>();
        }

        private void OnRaiseMessageLoaded(Message e)
        {
            EventHandler<Message> handler = RaiseMessageLoaded;
            handler?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task LoadRecentMessage()
        {
            IsLoadingMore = true;

            _recentChatList = await GetRecentChats();
            _messageList = await MapRecentChatToMessageType(_recentChatList);

           await LoadMeesagesByPage(_page, _pageSize);

              
        }

       
        public async Task LoadMeesagesByPage(int pageIndex, int pageSize)
        {
            try
            {

                await Task.Run(() =>
                {
                    IsLoadingMore = true;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                       
                        var messageList = GetItemsAsync(pageIndex, pageSize);
                        if (Messages.Count == 0)
                        {
                            Messages.AddRange(messageList);
                        }
                        else
                        {
                            foreach (var item in messageList)
                            {
                                Messages.Insert(0, item);
                            }
                        }


                    });

                });

               

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<List<RecentChat>> GetRecentChats()
        {
            try
            {
                // TODO only load chats under respective groupId
                return await App.ServiceManager.GetRecentChats(null);

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

                return await Task.Run(() => {

                    return recentChats?.Where(c => c.GroupId == GroupId)
                        .OrderByDescending(o => o.MessageDate )
                        ?.Select(s =>
                            new Message
                            {
                                Text = s.LastMessage,
                                IsIncoming = s.IsInComming,
                                MessageDateTime = s.MessageDate,
                                SenderId = s.SenderId
                            })?.ToList();
                });

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private List<Message> GetItemsAsync(int pageIndex, int pageSize)
        {
            if (_messageList.Count == 0) return new List<Message>();

            if (_messageList.Skip(pageIndex * pageSize) == null) return new List<Message>();

            return _messageList.Skip(pageIndex * pageSize)?.Take(pageSize)?.ToList();
        }
    }

}
