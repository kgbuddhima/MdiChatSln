using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Services
{
   public interface ISignalRClient
   {
       void ConnectToServer(string authToken);
       void Invoke(string methodName, params Object[] args);
   }
}
