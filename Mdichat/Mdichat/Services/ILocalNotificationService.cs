using Mdichat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.Services
{
    public interface ILocalNotificationService
    {
        void Show(NotificationData notificationData);
    }
}
