using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.MdiWebService.DTO
{
    public class UserRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public DateTime BirthDate { get; set; }
        public string DeviceToken { get; set; }
        public byte[] UserImage { get; set; }
    }
}
