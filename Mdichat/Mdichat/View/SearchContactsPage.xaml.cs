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
    public partial class SearchContactsPage : ContentPage
    {
        ContactsViewModel vm;
        public SearchContactsPage()
        {
            InitializeComponent();
            BindingContext = vm = new ContactsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindContacts();
        }

        private void BindContacts()
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                vm.ContactsCollection = await GetContactsAsync();
            });
            
        }

        /// <summary>
        /// Get Messages to Message List : not using
        /// </summary>
        /// <returns></returns>
        private async Task<List<ContactModel>> GetContactsAsync()
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

        private async void TxtSearchStr_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var result = await App.ServiceManager.SearchContacts(TxtSearchStr.Text);
            listViewContactList.ItemsSource = result;
        }

        private void Cell_OnTapped(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }
    }

    /*
    class SearchContactsPageViewModel : INotifyPropertyChanged
    {

        public SearchContactsPageViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
    */
}
