using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using MdiChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchAddMembersToChatPage : ContentPage
    {
        public delegate void SetMemberEventHandler(object source, EventArgs args);
        public event SetMemberEventHandler MemberSet;

        List<MdiContact> _allContacts = new List<MdiContact>();
        private ObservableCollection<ContactGroupModel> gm = null;
        ContactListVM contactGroupsVm = new ContactListVM();

        public SearchAddMembersToChatPage()
        {
            InitializeComponent();
            this.Title = "Select New Member";
            this.BindingContext = contactGroupsVm;
        }

        private async void tgrSearchImage_Tapped(object sender, EventArgs e)
        {
            contactGroupsVm.ContactGroups = await GetContactsAsync(TxtSearchStr.Text);
        }

        private void TxtSearchStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private async void gr_grdlistViewContacts_Tapped(object sender, EventArgs e)
        {
            try
            {
                var args = (TappedEventArgs)e;
                var parameters = (ParamObject)args.Parameter;
                if (parameters != null)
                {
                    MdiContact m = _allContacts.Where(o => o.ContactId == parameters.ContactId).FirstOrDefault();
                    if (m != null)
                    {
                        OnMemberSet(m);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error selecting member",ex.Message,"OK");
            }
            finally
            {
                await Navigation.PopAsync();
            }
        }

        protected virtual void OnMemberSet(MdiContact m)
        {
            if (MemberSet != null)
            {
                MemberSet(m, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get Contacts List with groups
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactGroupModel>> GetContactsAsync(string searchStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchStr))
                {
                    contactGroupsVm.IsBusy = true;
                    _allContacts = await App.ServiceManager.SearchContacts(searchStr);
                }
                if (_allContacts == null || _allContacts.Count == 0)
                {
                    return null;
                }

                gm = null;
                gm = new ObservableCollection<ContactGroupModel>();

                var set1 = _allContacts.ToList().OrderBy(x => x.Name);

                foreach (var item in set1)
                {
                    var firstLetter = item.Name.Substring(0, 1);

                    if (gm.Any(x => x.ContactShortTitle == firstLetter.ToUpper()))
                    {
                        continue;
                    }

                    var collection = new ContactGroupModel
                    {
                        ContactTitle = $"Starts with {firstLetter.ToUpper()}",
                        ContactShortTitle = firstLetter.ToUpper(),
                        Image = string.Format("{0}.png", firstLetter.ToUpper()) //"B32.png"
                    };

                    var filteredItems = _allContacts.Where(x => x.Name.StartsWith(firstLetter)).Select(x => new ContactModel
                    {
                        ContactID = x.ContactId,
                        ContactName = x.Name,
                        ContactNumber = x.ContactNumber,
                        FirstLetter = firstLetter,
                        Image = x.Image, // should have a byte array - buddhima 2018-03-31
                        Parameters = new ParamObject
                        {
                            ContactId = x.ContactId,
                            GroupId = (x.GroupId != null) ? x.GroupId.Value : 0,
                            UserName = x.UserName
                        }

                    });

                    foreach (var fitem in filteredItems)
                    {
                        collection.Add(fitem);
                    }

                    // result.RemoveAll(x => x.Name.StartsWith(firstLetter));
                    if (collection.Count > 0)
                    {
                        gm.Add(collection);
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("", "Error searching...", "OK");
            }
            finally
            {
                contactGroupsVm.IsBusy = false;
            }
            return gm;
        }
    }
}