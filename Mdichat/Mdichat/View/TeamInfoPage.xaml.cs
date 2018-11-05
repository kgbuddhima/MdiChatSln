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
using MdiChat.ViewModel;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamInfoPage : ContentPage
    {
        #region Properties

        TeamInfoViewModel vm = new TeamInfoViewModel();

        #endregion

        #region Constructors

        public TeamInfoPage(int groupId)
        {
            InitializeComponent();
            vm.AdminsCollection = new ObservableCollection<ContactModel>();
            vm.MembersCollection = new ObservableCollection<ContactModel>();
            this.BindingContext = vm;
            vm.GroupName = "Buddhima,Jeevan";
            grdTeamTbConformInfo.IsVisible = false;
            //this.Title = 
            ToolbarItems.Add(new ToolbarItem() { Icon = "Setting64B.png" });           
        }

        #endregion

        #region Events

        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*listViewAdmins.ItemsSource = GetAdmins();
            listViewTeamMembers.ItemsSource = GetTeamMembers();*/
            vm.AdminsCollection = GetAdmins();
            vm.MembersCollection = GetTeamMembers();
        }

        private void btnTeamTb_ConformTeamInfo_Clicked(object sender, EventArgs e)
        {
            vm.GroupName = txtNewTeamName.Text.Trim();
            grdTeamTbConformInfo.IsVisible = false;
        }

        private void btnTeamTb_CancelTeamInfo_Clicked(object sender, EventArgs e)
        {
            txtNewTeamName.Text = string.Empty;
            grdTeamTbConformInfo.IsVisible = false;
        }

        private void tgrImgTeam_Tapped(object sender, EventArgs e)
        {
            grdTeamTbConformInfo.IsVisible = true;
        }

        private void tgrCategoryNxtImg_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategorySelectPage());
        }

        /// <summary>
        /// navigate to TeamInfoChangeDescPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tgrTeamDscNxtImg_Tapped(object sender, EventArgs e)
        {
            try
            {
                var dscpage = new TeamInfoChangeDescPage(Convert.ToString(vm.PatientDemographics));
                dscpage.DescriptionChange += OnDemographicChanged;
                await Navigation.PushAsync(dscpage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void tgrFilesNxtImg_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagePage());
        }

        /// <summary>
        /// Receive new demographic text from navigated page
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnDemographicChanged(object source, EventArgs e)
        {
            try
            {
                string m = (string)source;
                vm.PatientDemographics = m;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ContactModel> GetAdmins()
        {
            ObservableCollection<ContactModel> collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima Kudagama", Owner = "Me", FirstLetter = "B" });
                listViewAdmins.HeightRequest = (double)((40 * collection.Count) + (10 * collection.Count));
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ContactModel> GetTeamMembers()
        {
            ObservableCollection<ContactModel> collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima Kudagama", Owner = "Me" });
                collection.Add(new ContactModel() { ContactID = 2, ContactName = "Buddhika Muhandiram", Owner = "Member" });
                listViewTeamMembers.HeightRequest = (double)((40 * collection.Count) + (10 * collection.Count));
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }

        #endregion

    }
}
