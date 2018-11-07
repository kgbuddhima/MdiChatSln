using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin;
using Mdichat.Model;

namespace Mdichat.Services
{
    public interface IUserContactsService
    {
        Task<IEnumerable<ContactModel>> GetAllContacts();
        List<ContactModel> FindContacts(string searchInContactsString);
    }
}
