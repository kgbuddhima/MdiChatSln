using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.MdiWebService.DTO
{
    public class VerifyCode
    {
        public string DeviceToken { get; set; }
        public string Code { get; set; }
    }
}
