using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using MdiChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TbContactsPage : ContentPage
	{
		#region private properties ...

		List<MdiContact> _allContacts = new List<MdiContact>();
		// each users contacts
		List<MdiContact> _userContacts = new List<MdiContact>();
		List<MdiContact> _contactsCopy2 = new List<MdiContact>();
		private ObservableCollection<ContactGroupModel> gm = null;
		ContactListVM contactGroupsVm = new ContactListVM();
		private ObservableCollection<MdiContact> _contactsObservable = new ObservableCollection<MdiContact>();

		 
		#endregion

		#region Constructor

		public TbContactsPage()
		{
			InitializeComponent();
			this.BindingContext = contactGroupsVm;
		   
			grdReadyToMessage.IsVisible = false;
			contactGroupsVm.IsBusy = false;
			// _contactsObservable.CollectionChanged

		}
		#endregion

		protected override void OnAppearing()
		{
			
			base.OnAppearing();
			Device.BeginInvokeOnMainThread(async() => {
				contactGroupsVm.ContactGroups = await GetUserContacts();
			});
		  // var pages = Navigation.NavigationStack.ToList();
			
		}

		/// <summary>
		/// Get Contacts List with groups
		/// </summary>
		/// <returns></returns>
		private async Task<ObservableCollection<ContactGroupModel>> GetContactsAsync(string searchStr)
		{
			try
			{
			    var cachedUsers = Helpers.Settings.UserContacts ?? new List<MdiContact>();

				if (!string.IsNullOrEmpty(searchStr))
				{
					contactGroupsVm.IsBusy = true;
					_allContacts = await App.ServiceManager.SearchContacts(searchStr);
				}
				if (_allContacts == null || _allContacts.Count == 0)
				{
					return null;
				}

                cachedUsers.AddRange(_allContacts
                    .Where(x => cachedUsers.All(cu => x.ContactId != cu.ContactId)));

			    Helpers.Settings.UserContacts = cachedUsers;
			    Task.Run(() =>
			    {
			        SaveUserImagesAsFiles();
			    });
                //TODO refactoring needed.
                gm = null;
				gm = new ObservableCollection<ContactGroupModel>();

				GetContactsGroupModal(gm, _allContacts);
			}
			catch (Exception)
			{
			  await DisplayAlert("","Error searching...","OK");
			}
			finally
			{
				contactGroupsVm.IsBusy = false;
			}           
			return gm;
		}

	   
		private void grContactsTabAddBtn_Tapped(object sender, EventArgs e)
		{
			try
			{
				imgAddMessage.IsVisible = false;
				grdReadyToMessage.IsVisible = true;

			}
			catch (Exception)
			{

				throw;
			}
		}

		private void grCancelMessage_Tapped(object sender, EventArgs e)
		{
			try
			{
				imgAddMessage.IsVisible = true;
				grdReadyToMessage.IsVisible = false;
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void grCreateNewMessageBoxView_Tapped(object sender, EventArgs e)
		{
			try
			{
				imgAddMessage.IsVisible = true;
				grdReadyToMessage.IsVisible = false;
				Navigation.PushAsync(new MessagePage());
			}
			catch (Exception)
			{

				throw;
			}

		}

		private void grCreateNewMessageImg_Tapped(object sender, EventArgs e)
		{
			try
			{
				grdReadyToMessage.IsVisible = false;
				Navigation.PushAsync(new NewConversationPage());
				imgAddMessage.IsVisible = true;
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void grCreateNewMessageLbl_Tapped(object sender, EventArgs e)
		{
			try
			{
				grdReadyToMessage.IsVisible = false;
				Navigation.PushAsync(new NewConversationPage());
				imgAddMessage.IsVisible = true;
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Open Search contacts page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void contactsTab_SearchbarBoxView_Tapped(object sender, EventArgs e)
		{
			try
			{
				Navigation.PushAsync(new SearchContactsPage());
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Navigate to Message Page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gr_grdlistViewContacts_Tapped(object sender, EventArgs e)
		{
			var args = (TappedEventArgs)e;
			var parameters = (ParamObject)args.Parameter;
			Navigation.PushAsync(new MessagePage(parameters.ContactId, parameters.GroupId, parameters.UserName));
		}

		private async void TxtSearchStr_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}

		private async void tgrSearchImage_Tapped(object sender, EventArgs e)
		{
            Device.BeginInvokeOnMainThread(() =>
            {
                contactGroupsVm.ContactGroups = GetContactsGroupModal(null, _userContacts, TxtSearchStr.Text);
            });

			var remoteData = await GetContactsAsync(TxtSearchStr.Text);

		    foreach (var remoteContact in remoteData)
		    {
		        Device.BeginInvokeOnMainThread(() =>
		        {
		            contactGroupsVm.ContactGroups.Add(remoteContact);
		        });
            }
		    

        }

        private async Task<ObservableCollection<ContactGroupModel>> GetUserContacts()
		{
			try
			{
				contactGroupsVm.IsBusy = true;
			    _userContacts = Helpers.Settings.UserContacts;

                if (_userContacts == null)
			    {
			        _userContacts = await App.ServiceManager.GetUserContacts();
			        Helpers.Settings.UserContacts = _userContacts;
			        Task.Run(() =>
			        {
			            SaveUserImagesAsFiles();
			        });
                }
			    return GetContactsGroupModal(null, _userContacts);
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				contactGroupsVm.IsBusy = false;
			}
		}

		private ObservableCollection<ContactGroupModel> GetContactsGroupModal(
		   ObservableCollection<ContactGroupModel> contactsGroupModal,
		   IEnumerable<MdiContact> contacts, string searchText = null)
		{

			try
			{
			    IEnumerable<MdiContact> filteredContacts;

				if (contactsGroupModal == null)
				{
					contactsGroupModal = new ObservableCollection<ContactGroupModel>();
				}

			    if (searchText == null)
			    {
			        filteredContacts = contacts.ToList().OrderBy(x => x.Name);
			    }
			    else
			    {
			        filteredContacts = contacts.ToList().OrderBy(x => x.Name)
                        .Where(x => x.Name.Contains(searchText));

			    }

			    foreach (var item in filteredContacts)
				{
					var firstLetter = item.Name.Substring(0, 1);

					if (contactsGroupModal
						.Any(x => x.ContactShortTitle == firstLetter.ToUpper()))
					{
						continue;
					}

					var collection = new ContactGroupModel
					{
						ContactTitle = $"Starts with {firstLetter.ToUpper()}",
						ContactShortTitle = firstLetter.ToUpper(),
						Image = $"{firstLetter.ToUpper()}.png" //"B32.png"
					};

					var filteredItems = contacts
						.Where(x => x.Name.StartsWith(firstLetter))
						.Select(x => new ContactModel
						{
							ContactID = x.ContactId,
							ContactName = x.Name,
							ContactNumber = x.ContactNumber,
							FirstLetter = firstLetter,
							Image = x.Image, // should have a byte array - buddhima 2018-03-31
						Parameters = new ParamObject
							{
								ContactId = x.ContactId,
								GroupId = x.GroupId ?? 0,
								UserName = x.UserName
							}

						});

					foreach (var fitem in filteredItems)
					{
						collection.Add(fitem);
					}


					if (collection.Count > 0)
					{
						contactsGroupModal.Add(collection);
					}
				}

				return contactsGroupModal;
			}
			catch (Exception ex)
			{

				throw;
			}
		}


	    private async void SaveUserImagesAsFiles()
	    {
	        var cachedUsers = Helpers.Settings.UserContacts;

	        if (cachedUsers == null) return;

	        var usersWithoutSavedImages = cachedUsers.Where(u => string.IsNullOrEmpty(u.ImageFilePath));

	        foreach (var contact in usersWithoutSavedImages)
	        {
	            var fileName = await DependencyService.Get<IFileService>()
	                .SaveByteArrayAsImageFile(contact.UserImage, contact.ContactId.ToString());

	            cachedUsers.Find(x => x.ContactId == contact.ContactId).ImageFilePath = fileName;
	        }

	        Helpers.Settings.UserContacts = cachedUsers;


	    }

	}
}