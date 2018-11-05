using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin;
using MdiChat.Model;

namespace MdiChat.Services
{
    public interface IUserContactsService
    {
        Task<IEnumerable<ContactModel>> GetAllContacts();
        List<ContactModel> FindContacts(string searchInContactsString);
    }
}
