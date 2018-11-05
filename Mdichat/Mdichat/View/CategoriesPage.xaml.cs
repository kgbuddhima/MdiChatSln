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
    public partial class CategoriesPage : ContentPage
    {
        ObservableCollection<ContactModel> collection { get; set; }

        public CategoriesPage()
        {
            InitializeComponent();
            NavigationCommand = new Command(NavigationCommandToInfo);
            ToolbarItems.Add(new ToolbarItem() { Icon = "Add.png", Command = NavigationCommand });
            grdCategoryCreate.IsVisible = false;
            BindCategories();
            
           // BindingContext = new ContentPageViewModel();
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
        private async void BindCategories()
        {
            listViewCategoryList.ItemsSource = await GetCategories();
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactModel>> GetCategories()
        {
            collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Class ward 1", ContactNumber = "0 Teams" });
                collection.Add(new ContactModel() { ContactID = 2, ContactName = "Class ward 2", ContactNumber = "2 Teams" });
                collection.Add(new ContactModel() { ContactID = 3, ContactName = "Class ward 3", ContactNumber = "1 Team" });
                collection.Add(new ContactModel() { ContactID = 4, ContactName = "Class ward 4", ContactNumber = "3 Teams" });
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
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
                        DisplayAlert("Notification","Category Name is Required.","OK");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCategoryCreateCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdCategoryCreate.IsVisible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    /*
    class CategoriesPageViewModel : INotifyPropertyChanged
    {

        public CategoriesPageViewModel()
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
