using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdichat.MdiWebService.DTO;

//

namespace Mdichat.MdiWebService
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Conform email by security code
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ConfirmEmailAsync(string email, string code);

        /// <summary>
        /// Register with MDi by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> RequestRegisterEmailAsync(string email);

        Task<string> RegisterMobileNumber(MobileNoRegistration payload);
        Task<bool> ConfirmMobileNumber(VerifyCode payload);
        Task<MdiResponse> UpdateUserData(UserRegister payload);
        Task<MdiResponse> EditUserData(UserRegister payload);
        Task<MdiResponse> DeleteUnRegisteredUser(RegisterContact payload);
    }
}
