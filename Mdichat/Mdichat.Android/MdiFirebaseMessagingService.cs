/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Mdichat.Model;

namespace Mdichat.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MdiFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            ChatEventArgs chatArgs = new ChatEventArgs(
                                message.Data["message"].ToString()
                                , message.Data["groupId"].ToString()
                                , message.Data["contactId"] ?? ""
                                , message.Data["username"] ?? ""
                                , message.Data["chatId"] ?? ""
                                , message.Data["isfile"] ?? ""
                            );
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                Mdichat.Services.NotificationService.HandleNotification(chatArgs);

                //  SendNotification(message.Data["message"], message.Data["groupId"], message.Data["contactId"] ?? "", message.Data["username"] ?? "");
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal

                // Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                Mdichat.Services.NotificationService.HandleNotification(chatArgs);

            }

        }

        //void SendNotification(string messageBody, string groupId, string contactId = "", string username = "")
        //{
        //    var intent = new Intent(this, typeof(MainActivity));
        //    intent.AddFlags(ActivityFlags.ClearTop);
        //    intent.PutExtra("groupId", groupId);
        //    intent.PutExtra("contactId", contactId);
        //    intent.PutExtra("username", username);

        //    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
           
        //        var notificationBuilder = new Notification.Builder(this)
        //            .SetContentTitle("MDI Message")
        //            .SetSmallIcon(Resource.Drawable.ic_launcher)
        //            .SetContentText(messageBody)
        //            .SetAutoCancel(true)
        //            .SetCategory("Message")
        //           // .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
        //            .SetContentIntent(pendingIntent);

        //        var notificationManager = NotificationManager.FromContext(this);

        //        notificationManager.Notify(0, notificationBuilder.Build());
            
           
        //}
    }
}*/