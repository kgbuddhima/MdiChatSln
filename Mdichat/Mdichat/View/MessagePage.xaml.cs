using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MdiChat.Helpers;
using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using MdiChat.Services;
using MdiChat.ViewModel;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        public ICommand NavigationCommand { get; }
        public ICommand NavigationCommandInfo { get; }
        ChatStreamViewModel vm;
        private readonly ISignalRClient messageClient;
        private int _contactId;
        private int _groupId = 0;
        private string _userName = string.Empty;
        private int _chatId; 

        public MessagePage(int contactId = 1, int groupId = 0, string userName = "", int chatId = 0)
        {
            try
            {
                InitializeComponent();
                Title = "Messages";
                NavigationCommand = new Command(NavigationCommandToAddUser);
                NavigationCommandInfo = new Command(NavigationCommandToChatInfo);
                ToolbarItems.Add(new ToolbarItem() { Icon = ImgLib.InforNavbarImg, Command = NavigationCommandInfo });
                ToolbarItems.Add(new ToolbarItem() { Icon = ImgLib.AddMemberToChatnavBarImg, Command = NavigationCommand });
                // BindingContext = vm = new MainChatViewModel();
                BindingContext = vm = new ChatStreamViewModel();

                vm.ContactId = contactId;
                vm.GroupId = groupId;
                _userName = userName;
                _chatId = chatId;

                ChatClientFactory.GetChatClient().RaiseCustomEvent += HandleCustomEvent;

                NotificationService.RaiseCustomEvent += NotificationServiceOnRaiseCustomEvent;



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void NotificationServiceOnRaiseCustomEvent(object sender, ChatEventArgs args)
        {

            if (vm.GroupId == Convert.ToInt32(args.GroupId))
            {
                AddChatMessage(args);
            }
            else
            {
                NotificationService.SendDeviceNotification(args);
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.Load(_chatId);

            //Task.Run(() =>
            //{
            //   UpdateUserImages();
            //});
           // UpdateUserImages();

            //Task.Run(() =>
            //{
            //    UpdateChatImages();
            //});



        }

        protected override void OnDisappearing()
        {
            NotificationService.RaiseCustomEvent -= NotificationServiceOnRaiseCustomEvent;
            ChatClientFactory.GetChatClient().RaiseCustomEvent -= HandleCustomEvent;
            base.OnDisappearing();

        }


        private async void AddChatMessage(ChatEventArgs e)
        {
            var message = new Message
            {
                Text = e.Message,
                IsIncoming = true,
                MessageDateTime = DateTime.Now
                
            };
            Device.BeginInvokeOnMainThread(async() =>
            {
              // vm.Messages.Add(message);
                // get chat message from db
                int chatId = Convert.ToInt32(e.ChatId);
               // vm.Load(chatId);
                // if it is file update image
                // save chat message to local db
                // get chat by Id
                var result = await App.ServiceManager.GetChatMessageById(chatId);
                // save image 
                if (result.IsFile)
                {
                    var imageFileName = await DependencyService.Get<IFileService>()
                     .SaveByteArrayAsImageFile(result.FileData, $"{result.SenderId}{result.MessageDate.Ticks}");
                    result.ImageFilePath = imageFileName;

                }
                message.ImageFilePath = result.ImageFilePath;
                message.MessageDateTime = result.MessageDate;
                message.IsIncoming = true;
                vm.Messages.Add(message);

                vm.SaveChatMessageToLocalDb(result);
                MessagesListView.ScrollTo(vm.Messages[vm.Messages.Count - 1], ScrollToPosition.End, true);
            });
        }


        private void UpdateUserImages()
        {

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (vm.Messages == null || vm.Messages.Count == 0) return true;

                foreach (var vmMessage in vm.Messages)
                {
                    if (vmMessage.UserImage == null || vmMessage.UserImage.Length == 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            vmMessage.UserImagePath = getContactUserImage(vmMessage.SenderId);
                        });

                    }
                }
                return false;
            });
        }

        private async void UpdateChatImages()
        {
            foreach (var vmMessage in vm.Messages)
            {
                if (vmMessage.IsFileIsImage && vmMessage.FileData != null
                     && vmMessage.FileData.Length == 0
                     && string.IsNullOrEmpty(vmMessage.ImageFilePath))
                {
                    var fileName = await DependencyService.Get<IFileService>()
                        .SaveByteArrayAsImageFile(vmMessage.FileData, "test");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        vmMessage.ImageFilePath = fileName;
                    });

                }
            }
        }

        void HandleCustomEvent(object sender, ChatEventArgs e)
        {
            try
            {

                var message = new Message
                {
                    Text = e.Message,
                    IsIncoming = true,
                    MessageDateTime = DateTime.Now
                };
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.Messages.Add(message);
                    MessagesListView.ScrollTo(vm.Messages[vm.Messages.Count - 1], ScrollToPosition.End, true);
                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void txtMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void tgrAddImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                string fileName = fileData.FileName;
                string contents = Encoding.UTF8
                    .GetString(fileData.DataArray, 0,
                    fileData.DataArray.Length);

             
                var message = new Message
                {
                    Text = fileName,
                    IsIncoming = false,
                    MessageDateTime = DateTime.Now,
                    FileData = fileData.DataArray
                };

                var imageFileName = await DependencyService.Get<IFileService>()
                    .SaveByteArrayAsImageFile(fileData.DataArray, $"{vm.SenderId}{message.MessageDateTime.Ticks}");
                message.ImageFilePath = imageFileName;


                AddMessageToList(message);

                var chatMessage = new ChatMessage
                {
                    Message = fileName,
                    GroupId = vm.GroupId,
                    IsFile = true,
                    FileData = fileData.DataArray,
                    FileName = fileName,
                    imageFilePath = imageFileName
                    
                };
                chatMessage.Members.Add(new ChatReciever { Id = vm.ContactId, UserName = _userName });

                var response = await App.ServiceManager.SaveChatMessage(chatMessage);
                vm.GroupId = response.GroupId;

                chatMessage.ChatId = response.ChatId;

                SaveChatMessageToLocalDb(chatMessage);
            }
            catch (Exception ex)
            {

            }
        }

        private async void tgrSend_Tapped(object sender, EventArgs e)
        {
            try
            {
                var message = new Message
                {
                    Text = txtMessage.Text,
                    IsIncoming = false,
                    MessageDateTime = DateTime.Now
                };

                AddMessageToList(message);

                var chatMessage = new ChatMessage
                {
                    Message = txtMessage.Text,
                    GroupId = vm.GroupId,
                    MessageDateTime = message.MessageDateTime

                };
                chatMessage.Members.Add(new ChatReciever { Id = vm.ContactId, UserName = _userName });

               var response = await App.ServiceManager.SaveChatMessage(chatMessage);
                vm.GroupId = response.GroupId;

                chatMessage.ChatId = response.ChatId;

                SaveChatMessageToLocalDb(chatMessage);

                txtMessage.Text = string.Empty;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SaveChatMessageToLocalDb(ChatMessage chatMessage)
        {
            RecentChat recentChat = new RecentChat();

            recentChat.Id = chatMessage.ChatId;
            recentChat.LastMessage = chatMessage.Message;
            recentChat.GroupId = chatMessage.GroupId;
            recentChat.MessageDate = chatMessage.MessageDateTime;
            recentChat.FileData = chatMessage.FileData;
            recentChat.FileName = chatMessage.FileName;
            recentChat.IsInComming = false;
            recentChat.IsFile = chatMessage.IsFile;
            recentChat.ImageFilePath = chatMessage.imageFilePath;

            vm.SaveChatMessageToLocalDb(recentChat);
        }

        private void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        private void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var message = new Message
                {
                    Text = txtMessage.Text,
                    IsIncoming = false,
                    MessageDateTime = DateTime.Now
                };

                AddMessageToList(message);

                var chatMessage = new ChatMessage
                {
                    Message = txtMessage.Text,
                    GroupId = vm.GroupId,

                };
                chatMessage.Members.Add(new ChatReciever { Id = vm.ContactId, UserName = _userName });

               // vm.GroupId = await App.ServiceManager.SaveChatMessage(chatMessage);


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddMessageToList(Message message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                vm.Messages.Add(message);
                MessagesListView.ScrollTo(vm.Messages[vm.Messages.Count - 1], ScrollToPosition.End, true);
            });
        }

        private async void Btn_FilePicker(object sender, EventArgs eargs)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                string fileName = fileData.FileName;
                string contents = Encoding.UTF8.GetString(fileData.DataArray, 0, fileData.DataArray.Length);

                var message = new Message
                {
                    Text = fileName,
                    IsIncoming = false,
                    MessageDateTime = DateTime.Now
                };

                AddMessageToList(message);

                var chatMessage = new ChatMessage
                {
                    Message = fileName,
                    GroupId = vm.GroupId,
                    IsFile = true,
                    FileData = fileData.DataArray,
                    FileName = fileName
                };
                chatMessage.Members.Add(new ChatReciever { Id = vm.ContactId, UserName = _userName });

               // vm.GroupId = await App.ServiceManager.SaveChatMessage(chatMessage);
            }
            catch (Exception e)
            {

            }
        }

        private void NavigationCommandToAddUser() => AddUser();

        /// <summary>
        /// Navigate to teamInfo page
        /// </summary>
        private async void NavigationCommandToChatInfo()
        {
            /* StringBuilder builder = new StringBuilder();
             foreach (MdiContact c in vm?.GroupMembersCollection)
             {
                 builder.AppendLine(c.Name);
             }
             DisplayAlert("", builder.ToString(), "OK");*/
            await Navigation.PushAsync(new TeamInfoPage(vm.GroupId), true);
        }

        /// <summary>
        /// Navigate to SearchAddMembersToChatPage and select new members to the chat
        /// </summary>
        private async void AddUser()
        {
            var addMemberpage = new SearchAddMembersToChatPage();
            addMemberpage.MemberSet += this.OnMemberSet;
            await Navigation.PushAsync(addMemberpage);
        }

        /// <summary>
        /// Get new members from SearchAddMembersToChatPage
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnMemberSet(object source, EventArgs e)
        {
            try
            {
                MdiContact m = (MdiContact)source;
                vm.GroupMembersCollection = vm.GroupMembersCollection ?? new ObservableCollection<MdiContact>();
                vm.GroupMembersCollection.Add(m);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getContactUserImage(int contactId)
        {
            var cachedContacts = Helpers.Settings.UserContacts;
            return cachedContacts == null ?
                string.Empty :
                cachedContacts.Find(u => u.ContactId == contactId)?.ImageFilePath;
        }

    }
}