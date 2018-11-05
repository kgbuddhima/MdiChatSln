using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Model
{
    public static class Constants
    {
        // URL of REST service
        public static string BaseUrl = "http://jeevan90-002-site1.etempurl.com/";
        //public static string BaseUrl = "http://localhost:4600/";
        public static string RegisterMobileUrl = "api/account/registermobile";
        public static string VerifyMobileUrl = "api/account/VerifyRegistrationCode";
        public static string UpdateUserUrl = "api/account/updateuserdata";
        public static string DeleteUnregistredUserUrl = "api/account/DeleteUnregistered";
        public static string EditUserUrl = "api/account/edituserdata";
        public static string UserLoginUrl = "Token";
        public static string GetUserUrl = "api/account/getuser";
        public static string ForgotPasswordUrl = "api/account/ForgotPassword";
        public static string ResetPasswordUrl = "api/account/ResetPassword";
        public static string SearchContactsUrl = "api/chat/SearchContacts";
        public static string SaveChatMessageUrl = "api/chat/SaveChatMessage";
        public static string GetRecentChatsUrl = "api/chat/GetRecentChats";
        public static string GetChatMessageById = "api/chat/GetChatMessageById";
        public static string GetChatsByGroupId = "api/chat/GetChatsByGroupId";
        public static string RegisterNotificationHubTokenUrl = "api/chat/RegisterNotificationHubToken";
        public static string GetUserContacts = "api/chat/GetUserContacts";
        public static string GetChatGroups = "api/Chat/GetChatGroups";

        public static string urlRegsterEmail = "api/account/registerEmail";
        public static string urlConfirmEmail = "api/account/confirmEmail";
        // Credentials that are hard coded into the REST service
        public static string Username = "Xamarin";
        public static string Password = "Pa$$w0rd";

        public static string S_Exists = "EXIST";
        public static string S_OK = "OK";
        public const string S_Error = "ERROR";
        public static string S_NotSuccess = "NOT_SUCCESS";
        public const string S_Retry = "RETRY";

        public static string Msg_Email_Exists = "The email already exists.";
        public static string Msg_Request_Failed = "Request failed.";
        public const string Msg_PermissionCode_worng = "Wrong Permission Code, Try again !";
    }
}
