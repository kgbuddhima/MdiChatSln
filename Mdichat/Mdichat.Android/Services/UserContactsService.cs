using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MdiChat.Model;
using MdiChat.Services;
using Xamarin.Forms;
using Xamarin.Contacts;

[assembly: Dependency(typeof(MdiChat.Droid.Services.UserContactsService))]
namespace MdiChat.Droid.Services
{
    public class UserContactsService : IUserContactsService
    {
        private AddressBook _book;

        private static IEnumerable<ContactModel> _contacts;

        public UserContactsService()
        {

        }

        public List<ContactModel> FindContacts(string searchInContactsString)
        {
            _book = new AddressBook(Forms.Context.ApplicationContext);
            var ResultContacts = new List<ContactModel>();

            foreach (var currentContact in _contacts)
            {
                // Running a basic String Contains() search through all the 
                // fields in each Contact in the list for the given search string
                if ((currentContact.Contact_FirstName != null && currentContact.Contact_FirstName.ToLower().Contains(searchInContactsString.ToLower())) ||
                    (currentContact.Contact_LastName != null && currentContact.Contact_LastName.ToLower().Contains(searchInContactsString.ToLower())) ||
                    (currentContact.Contact_EmailId != null && currentContact.Contact_EmailId.ToLower().Contains(searchInContactsString.ToLower())))
                {
                    ResultContacts.Add(currentContact);
                }
            }

            return ResultContacts;
        }

        public async Task<IEnumerable<ContactModel>> GetAllContacts()
        {
            _book = new AddressBook(Forms.Context.ApplicationContext);
            if (_contacts != null) return _contacts;
            try
            {
                var contacts = new List<ContactModel>();
                await _book.RequestPermission().ContinueWith(t =>
                {
                    if (!t.Result)
                    {
                        Console.WriteLine("Sorry ! Permission was denied by user or manifest !");
                        return;
                    }
                    foreach (Contact contact in _book.ToList()) // Filtering the Contact's that has E-Mail addresses   Where(c => c.Emails.Any())
                    {
                      //  var firstOrDefault = contact.Emails.FirstOrDefault();
                     //   if (firstOrDefault != null)
                     //   {
                            contacts.Add(new ContactModel()
                            {
                                Contact_FirstName = contact.FirstName,
                                Contact_LastName = contact.LastName,
                                Contact_DisplayName = contact.DisplayName,
                                Contact_EmailId = contact.Emails.Count()>0? contact.Emails.FirstOrDefault().Address:string.Empty, //firstOrDefault.Address,
                                ContactNumber = contact.Phones.Count() > 0 ? contact.Phones.FirstOrDefault().Number:string.Empty,
                            });
                      //  }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());

                _contacts = (from c in contacts orderby c.Contact_DisplayName select c).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _contacts;
        }
    }
}