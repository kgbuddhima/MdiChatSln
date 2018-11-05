/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MdiChat.Droid;
using MdiChat.Model;
using MdiChat.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotificationService))]
namespace MdiChat.Droid
{
    
    public class LocalNotificationService : ILocalNotificationService
    {
        private Object thisLock = new Object();
        public void Show(NotificationData notificationData)
        {

            var intent = new Intent(Application.Context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("groupId", notificationData.GroupId);
            intent.PutExtra("contactId", notificationData.ContactId);
            intent.PutExtra("username", notificationData.UserName);
            intent.PutExtra("chatId", notificationData.ChatId);


            var pendingIntent = PendingIntent.GetActivity(Application.Context,
                0, intent, PendingIntentFlags.OneShot);

            lock (thisLock)
            {
        

                var notificationBuilder = new Notification.Builder(Application.Context)
                        .SetContentTitle("MDI Message")
                        .SetSmallIcon(Resource.Drawable.icon)
                        .SetContentText(notificationData.Message)
                        .SetPriority(1)
                        //.
                        .SetCategory("Message") // this will pop up down the message bar
                        .SetAutoCancel(true)
                        .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                        .SetContentIntent(pendingIntent)
                    ;
                    //.SetColor()

                var notificationManager = NotificationManager.FromContext(Application.Context);

                notificationManager.Notify(0, notificationBuilder.Build());
            }
        }
    }
}*/