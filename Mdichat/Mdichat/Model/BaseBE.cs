using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Model
{
    public class BaseBE
    {

    }

    public class ConfirmEmail
    {
        public string Email { get; set; }

        public string Code { get; set; }
    }

    public class RegisterEmail
    {
        public string Email { get; set; }
    }

}
