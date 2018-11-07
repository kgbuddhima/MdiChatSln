using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Services
{
    public interface INotificationHubService
    {
        void UpdateDeviceToken(string userName);
    }
}
