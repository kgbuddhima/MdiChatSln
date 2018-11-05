using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.MdiWebService.DTO;

namespace MdiChat.MdiWebService
{
    public interface IUserService
    {
        Task<MdiResponse> Login(UserLogin payload);
        Task<MdiUser> GetUser();
        Task<MdiResponse> ForgotPassword(ForgotPassword payload);
        Task<bool> ResetPassword(PasswordReset payload);
    }
}
