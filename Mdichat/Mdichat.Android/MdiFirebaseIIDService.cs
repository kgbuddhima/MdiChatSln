/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Mdichat.Services;

namespace Mdichat.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MdiFirebaseIIDService :  FirebaseInstanceIdService
    {
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(refreshedToken);
            ChatClientFactory.GetChatClient().SetFirebaseToken(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
           
            hub = new NotificationHub(Constants.NotificationHubName,
                Constants.ListenConnectionString, this);

            var tags = new List<string>() { "android" };
            var regID = hub.Register(token, tags.ToArray()).RegistrationId;
            ChatClientFactory.GetChatClient().SetNotificationRegistrationId(regID);

        }
    }
}*/