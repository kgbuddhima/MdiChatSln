using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.MdiWebService.DTO
{
    public class PasswordReset
    {
        public string Code { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MdiResetCode { get; set; }
        public string DeviceToken { get; set; }
        
    }
}
