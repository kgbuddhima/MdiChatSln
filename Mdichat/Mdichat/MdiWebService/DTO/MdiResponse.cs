using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.MdiWebService.DTO
{
    public class MdiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string AuthToken { get; set; }
        public string ResetCode { get; set; }
        public string DeviceToken { get; set; }
    }
}
