using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MdiChat.Droid
{
    public static class Constants
    {
        public const string ListenConnectionString = "Endpoint=sb://meditechdata.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=eDMzhhCU/TfOX/Wq33z7B6a4mDNQxiwkqSF1hZyzAqo=";
        public const string NotificationHubName = "meditechdata";
    }
}