using MdiChat.Model;
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
    public partial class CategorySelectPage : ContentPage
    {
        ObservableCollection<ContactModel> collection { get; set; }
        public CategorySelectPage()
        {
            InitializeComponent();
            // BindingContext = new CategorySelectPageViewModel();
            NavigationCommand = new Command(NavigationCommandToInfo);
            ToolbarItems.Add(new ToolbarItem() { Icon = "Add.png", Command = NavigationCommand });
            grdCategoryCreate.IsVisible = false;
            BindCategories();

        }

        public ICommand NavigationCommand { get; }
        void NavigationCommandToInfo() => ShowCreateCategoryDialague();

        private void ShowCreateCategoryDialague()
        {
            grdCategoryCreate.IsVisible = true;
        }

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private void BindCategories()
        {
            listViewCategoryList.ItemsSource = GetMessages();
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ContactModel> GetMessages()
        {
            collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Category KGB", ContactNumber = "0 Teams" });
                collection.Add(new ContactModel() { ContactID = 2, ContactName = "Category Buddhma", ContactNumber = "2 Teams" });
                collection.Add(new ContactModel() { ContactID = 3, ContactName = "Category Doe", ContactNumber = "1 Team" });
                collection.Add(new ContactModel() { ContactID = 4, ContactName = "Category DR", ContactNumber = "3 Teams" });
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }

        private void btnCategoryCreateCancel_Clicked(object sender, EventArgs e)
        {
            grdCategoryCreate.IsVisible = false;
        }

        private void btnCategoryCreateSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (collection != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtCategoryName.Text))
                    {
                        collection.Add(new ContactModel() { ContactID = 1, ContactName = txtCategoryName.Text, ContactNumber = "0 Teams" });
                        grdCategoryCreate.IsVisible = false;
                        txtCategoryName.Text = string.Empty;
                    }
                    else
                    {
                        DisplayAlert("Notification", "Category Name is Required.", "OK");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    /*class CategorySelectPageViewModel : INotifyPropertyChanged
    {

        public CategorySelectPageViewModel()
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

    }*/
}