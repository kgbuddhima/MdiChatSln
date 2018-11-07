using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.MdiWebService.DTO
{
    public class ChatMessagePayload
    {
        public int GroupId { get; set; }
        public int[] Members { get; set; }
        public string Message { get; set; }
    }
}
