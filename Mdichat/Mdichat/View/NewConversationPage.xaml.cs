using MdiChat.Model;
using MdiChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewConversationPage : ContentPage
    {
        ContactsViewModel vm;
        IEnumerable<ContactModel> ContactCollection { get; set; }

        public NewConversationPage()
        {
            ContactCollection = null;
            InitializeComponent();
            BindingContext = vm = new ContactsViewModel();

            NavigationCommand = new Command(NavigationCommandToInfo);
            ToolbarItems.Add(new ToolbarItem() { Icon="", Text = "Done", Command = NavigationCommand });           
        }

        public ICommand NavigationCommand { get; }

        void NavigationCommandToInfo() =>
            Navigation.PopAsync();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindAllAsync();
        }

        /// <summary>
        /// Bind Contacts to contact list
        /// </summary>
        private async void BindContactsAsync()
        {
           // listViewContactList.ItemsSource = await GetContacts();
            vm.ContactsCollection = await GetContactsAsync();
        }

        private async void BindTeams()
        {
            // listViewContactList.ItemsSource = await GetTeams();
            /// listViewContactList.ItemsSource = await GetContacts();
            vm.ContactsCollection= await GetContactsAsync();
        }

        private async void BindAllAsync()
        {
            // listViewContactList.ItemsSource = await GetContactsAndTeams();
            // listViewContactList.ItemsSource = await GetContacts();
            vm.ContactsCollection = await GetContactsAsync();
        }

        /// <summary>
        /// Get Contacts
        /// </summary>
        /// <returns></returns>
        private async Task<List<ContactModel>> GetContactsAsync()
        {
          //  IEnumerable<ContactModel> gm = new List<ContactModel>();
            try
            {
                if (ContactCollection == null)
                {
                    ActivityIndicatorVisibility(true);
                    ContactCollection = await DependencyService.Get<Services.IUserContactsService>().GetAllContacts();
                    if (ContactCollection!=null)
                    {
                        foreach (ContactModel model in ContactCollection) { model.Image = "ContactIcon.png"; }
                        ContactCollection = ContactCollection.OrderBy(o => o.Contact_DisplayName).ToList();
                        ActivityIndicatorVisibility(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityIndicatorVisibility(false);
                throw ex;
               
            }
            return ContactCollection.ToList();
        }

        /// <summary>
        /// Get teams
        /// </summary>
        /// <returns></returns>
        private List<ContactModel> GetTeams()
        {
            List<ContactModel> gm = new List<ContactModel>();
            try
            {
                gm.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima,Jeevan", ContactNumber = "0713775822", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 2, ContactName = "Nicholas,Buddhima", ContactNumber = "0713775822", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 3, ContactName = "Team 1", ContactNumber = "0773093431", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 4, ContactName = "Niki,Jeevan", ContactNumber = "0714180595", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 5, ContactName = "Doctor Micle,Nurse Jane ", ContactNumber = "0773093431", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 6, ContactName = "Dr.Maven,Phy.Phillip", ContactNumber = "0714180595", Image = "Teams.png" });
                gm = gm.OrderBy(o => o.ContactName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gm;
        }

        /// <summary>
        /// Get Contacts List with teams
        /// </summary>
        /// <returns></returns>
        private List<ContactModel> GetContactsAndTeams()
        {
            List<ContactModel> gm = new List<ContactModel>();
            try
            {
                gm.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima 1", ContactNumber = "0713775822", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 2, ContactName = "Buddhika", ContactNumber = "0713775822", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 3, ContactName = "John Doe", ContactNumber = "0773093431", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 4, ContactName = "Jeevan", ContactNumber = "0714180595", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 5, ContactName = "John Mickle ", ContactNumber = "0773093431", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 6, ContactName = "Montana Home", ContactNumber = "0714180595", Image = "ContactIcon.png" });
                gm.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima,Jeevan", ContactNumber = "0713775822", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 2, ContactName = "Nicholas,Buddhima", ContactNumber = "0713775822", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 3, ContactName = "Team 1", ContactNumber = "0773093431", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 4, ContactName = "Niki,Jeevan", ContactNumber = "0714180595", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 5, ContactName = "Doctor Micle,Nurse Jane ", ContactNumber = "0773093431", Image = "Teams.png" });
                gm.Add(new ContactModel() { ContactID = 6, ContactName = "Dr.Maven,Phy.Phillip", ContactNumber = "0714180595", Image = "Teams.png" });
                gm = gm.OrderBy(o => o.ContactName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gm;
        }

        /// <summary>
        /// Change color of menu items
        /// </summary>
        /// <param name="btn"></param>
        private void ChangeTeamTabPatientAllColor(Button btn)
        {
            try
            {
                if (btn.BackgroundColor == Color.White)
                {
                    if (btn == btnAll)
                    {
                        btnAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                        btnContacts.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                        btnTeams.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
                    else if (btn == btnContacts)
                    {
                        btnContacts.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                        btnAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                        btnTeams.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
                    else
                    {
                        btnTeams.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                        btnAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                        btnContacts.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Show hide acrivity indicator
        /// </summary>
        /// <param name="active"></param>
        private void ActivityIndicatorVisibility(bool active)
        {
            actSendingCode.IsVisible = active;
            actSendingCode.IsRunning = active;
        }

        private void listViewContactList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // don't do anything if we just de-selected the row
            if (e.Item == null) return;
            // do something with e.SelectedItem
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        private void btnAll_Clicked(object sender, EventArgs e)
        {
            ChangeTeamTabPatientAllColor(btnAll);
            BindAllAsync();
        }

        private void btnContacts_Clicked(object sender, EventArgs e)
        {
            ChangeTeamTabPatientAllColor(btnContacts);
            BindContactsAsync();
        }

        private void btnTeams_Clicked(object sender, EventArgs e)
        {
            ChangeTeamTabPatientAllColor(btnTeams);
            BindTeams();
        }
    }
}
