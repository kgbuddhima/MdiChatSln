using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Mdichat.MdiWebService.DTO
{
    public class MdiContact
    {
        [PrimaryKey]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public string Contact_Picture { get; set; }
        public string ContactNumber { get; set; }
        public int? GroupId { get; set; }
        public string UserName { get; set; }
        public byte[] UserImage { get; set; }
        public string ImageFilePath { get; set; }
    }
}
