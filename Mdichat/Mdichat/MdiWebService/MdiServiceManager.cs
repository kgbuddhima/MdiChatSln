using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdichat.MdiWebService.DTO;
using Mdichat.Model;

//

namespace Mdichat.MdiWebService
{
    public class MdiServiceManager
    {
        IRegistrationService _registrationService;
        private IUserService _userService;
        private IChatService _chatService;

        /// <summary>
        /// Constructor Mdi Service Manager
        /// </summary>
        /// <param name="registrationService"></param>
        /// <param name="userService"></param>
        /// <param name="chatService"></param>
        public MdiServiceManager(IRegistrationService registrationService, IUserService userService, IChatService chatService)
        {
            _registrationService = registrationService;
            _userService = userService;
            _chatService = chatService;
        }

        /// <summary>
        /// Conform email by security code
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<bool> ConfirmEmailAsync(string email, string code)
        {
            try
            {
                return _registrationService.ConfirmEmailAsync(email, code);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Register with MDi by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<string> RequestRegisterEmailAsync(string email)
        {
            try
            {
                return _registrationService.RequestRegisterEmailAsync(email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<string> RequestRegisterMobileAsync(MobileNoRegistration payload)
        {
            try
            {
                return _registrationService.RegisterMobileNumber(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //
        public Task<bool> ConfirmMobileNumber(VerifyCode payload)
        {
            try
            {
                return _registrationService.ConfirmMobileNumber(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #region <User>
        public Task<MdiResponse> UpdateUserData(UserRegister payload)
        {
            try
            {
                return _registrationService.UpdateUserData(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<MdiResponse> DeleteUnRegisteredUser(RegisterContact payload)
        {
            try
            {
                return _registrationService.DeleteUnRegisteredUser(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<MdiResponse> Login(UserLogin payload)
        {
            try
            {
                return _userService.Login(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<MdiUser> GetUSer()
        {
            try
            {
                return _userService.GetUser();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<MdiResponse> ForgotPassword(ForgotPassword payload)
        {
            try
            {
                return _userService.ForgotPassword(payload);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<bool> ResetPassword(PasswordReset payload)
        {
            try
            {
                return _userService.ResetPassword(payload);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Task<MdiResponse> EditUserData(UserRegister payload)
        {
            try
            {
                return _registrationService.EditUserData(payload);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        public Task<List<MdiContact>> SearchContacts(string searchStr)
        {
            try
            {
                return _chatService.SearchContacts(searchStr);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<ChatResponse> SaveChatMessage(ChatMessage payload)
        {
            try
            {
                return _chatService.SaveChatMessage(payload);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<List<RecentChat>> GetRecentChats(ChatMessage payload)
        {
            try
            {
                return _chatService.GetRecentChats();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RegisterNotificationHub(NotificationHubRegistration payload)
        {
            try
            {
                return await _chatService.RegisterNotificationHub(payload);
            }
            catch (Exception e)
            {
               throw e;
            }
        }

        public Task<List<MdiContact>> GetUserContacts()
        {
            try
            {
                return _chatService.GetUserContacts();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the collection of chat groups of an user
        /// </summary>
        /// <returns></returns>
        public Task<List<ChatGroup>> GetChatGroups()
        {
            try
            {
                return _chatService.GetChatGroups();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<RecentChat> GetChatMessageById(int chatId)
        {
            try
            {
                return _chatService.GetChatMessageById(chatId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<List<RecentChat>> GetChatMessagesByGroupId(int groupId)
        {
            try
            {
                return _chatService.GetChatMessagesByGroupId(groupId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
