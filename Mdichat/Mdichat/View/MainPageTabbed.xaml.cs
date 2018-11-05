using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Model;
using MdiChat.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTabbed : TabbedPage
    {
        public MainPageTabbed(bool fromNotification = false, int contactId = 1, int groupId = 0, string userName = "", int chatId = 0)
        {
            InitializeComponent();
            ListenForChatNotifications();
            NavigationPage.SetHasNavigationBar(this, false);
            // sample code for recive onlie users
            ChatClientFactory.GetChatClient().OnlineUsersReceivedEvent += HandleOnlineUsers;

            if (fromNotification == true)
            {
                // NOTE: MessaginCenter had issue with two times calling. So Now using this line
                Navigation.PushAsync(new MessagePage(contactId, groupId, userName, chatId));

            }
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Task.Run(() =>
            //{
            //    DependencyService.Get<ISignalRClient>(DependencyFetchTarget.GlobalInstance)
            //        .ConnectToServer(Helpers.Settings.AuthToken);
            //});
        }
        protected override void OnDisappearing()
        {
            ChatClientFactory.GetChatClient().RaiseCustomEvent -= HandleNotificationRecievedEvent;
            base.OnDisappearing();
        }

        private void ListenForChatNotifications()
        {
            ChatClientFactory.GetChatClient().RaiseCustomEvent += HandleNotificationRecievedEvent;
        }

        void HandleNotificationRecievedEvent(object sender, ChatEventArgs e)
        {
            try
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    var data = new NotificationData
                    {
                        Message = e.Message,
                        ContactId = e.ContactId,
                        UserName = e.UserName,
                        GroupId = e.GroupId, 
                        ChatId = e.ChatId
                    };
                    DependencyService.Get<ILocalNotificationService>()
                    .Show(data);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void HandleOnlineUsers(object sender, OnlineUsersEventArgs e)
        {
            var users = string.Join(", ", e.Users.ToArray());
        }
    }

}