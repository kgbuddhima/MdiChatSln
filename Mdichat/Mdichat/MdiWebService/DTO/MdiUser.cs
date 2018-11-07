using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.MdiWebService.DTO
{
    public class MdiUser
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string AuthyUserId { get; set; }
        public string EmailCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] UserImage { get; set; }
        public DateTime? BirthDate { get; set; }
        public int MdiUserId { get; set; }
    }
}
