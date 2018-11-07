using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdichat.Model;
using Xamarin.Forms;

namespace Mdichat.Services
{
    public static class NotificationService
    {
        public static event EventHandler<ChatEventArgs> RaiseCustomEvent;

        public static void HandleNotification(ChatEventArgs e)
        {
            if (RaiseCustomEvent != null)
            {
                RaiseCustomEvent(null, e);
            }
            else
            {
                SendDeviceNotification(e);
            }


        }

        public static void SendDeviceNotification(ChatEventArgs e)
        {
            var data = new NotificationData
            {
                Message = e.Message,
                ContactId = e.ContactId,
                UserName = e.UserName,
                GroupId = e.GroupId, 
                ChatId = e.ChatId, 
                IsFile = e.IsFile
            };
            DependencyService.Get<ILocalNotificationService>()
                .Show(data);
        }
    }
}
