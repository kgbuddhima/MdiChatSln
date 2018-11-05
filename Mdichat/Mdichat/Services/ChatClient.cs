using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Model;

namespace MdiChat.Services
{
    public class ChatClient
    {
        
        public event EventHandler<ChatEventArgs> RaiseCustomEvent;

        public event EventHandler<OnlineUsersEventArgs> OnlineUsersReceivedEvent;

        public void RecieveMessage(string message, string groupId, string contactId, string userName, string chatId)
        {
            OnRaiseCustomEvent(new ChatEventArgs(message, groupId, contactId, userName, chatId, "false"));

        }
        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnRaiseCustomEvent(ChatEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<ChatEventArgs> handler = RaiseCustomEvent;
                                                       
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }
        protected virtual void OnRaiseOnlineUsersReceivedCustomEvent(OnlineUsersEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<OnlineUsersEventArgs> handler = OnlineUsersReceivedEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
               // e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        public void SetFirebaseToken(string token)
        {
            Helpers.Settings.FirebaseToken = token;
        }
        public void SetNotificationRegistrationId(string id)
        {
            Helpers.Settings.NotificationRegistrationId = id;
        }

        public void HandleOnlineUsers(List<string> onlineUsers)
        {
           OnRaiseOnlineUsersReceivedCustomEvent(new OnlineUsersEventArgs(onlineUsers));
        }

        public void ClearSubscribers()
        {
            RaiseCustomEvent = null;
        }
    }
}
