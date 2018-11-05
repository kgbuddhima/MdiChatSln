using MdiChat.MdiWebService.DTO;
using MdiChat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdiChat.ViewModel;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TbGroupsPage : ContentPage
    {
        GroupsPageVideModel vm = new GroupsPageVideModel();
        ObservableCollection<ContactGroupModel> gm = new ObservableCollection<ContactGroupModel>();
        List<ContactModel> _groupsCol = new List<ContactModel>();
        List<ContactModel> _groupsColFiltered = new List<ContactModel>();

        public TbGroupsPage()
        {
            InitializeComponent();
            vm.ContactGroups = new ObservableCollection<ContactGroupModel>();
            this.BindingContext = vm;

            grdTeamTabReadyToCreateTeams.IsVisible = false;
            grdTeamTbTeamTypeSelect.IsVisible = false;
            grdTeamTb_GroupCreate.IsVisible = false;
            //grdTeamTbCategoryCreate.IsVisible = false;
            //grdTeamTbConformInfo.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.NewGroupModel = null;
            vm.NewGroupModel = new NewGroupViewModel();
            BindTeams();
        }

        #region Team Tab ...

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private async void BindTeams()
        {
            if (gm != null) { gm.Clear(); }
            if (_groupsCol != null) { _groupsCol.Clear(); }
            if (_groupsColFiltered != null) { _groupsColFiltered.Clear(); }
            GetTeamsAsync();
            //listViewTeamList.ItemsSource = 
            vm.ContactGroups = await GetGroupsAsync(""); //await GetTeams();
        }

        /// <summary>
        /// Get Contacts List with groups
        /// </summary>
        /// <returns></returns>
        private async void GetTeamsAsync()//Task<ObservableCollection<ContactGroupModel>>
        {
            // ContactGroupModel contactCollection1 = new ContactGroupModel() { ContactTitle = "Class ward A", ContactShortTitle = "1 Team", Image = "B32.png" };
            // ContactGroupModel contactCollection2 = new ContactGroupModel() { ContactTitle = "Class ward B", ContactShortTitle = "3 Teams", Image = "J32.png" };
            try
            {
                /*_groupsCol.Add(new ContactModel() { ContactID = 1, ContactName = "Team 1", ContactNumber = "Buddhima,Buddhika" });
                _groupsCol.Add(new ContactModel() { ContactID = 2, ContactName = "Group 1", ContactNumber = "Doctor1,Nurse1" });
                _groupsCol.Add(new ContactModel() { ContactID = 3, ContactName = "Team 3", ContactNumber = "Doctor2,Nurse1,Buddhima" });
                _groupsCol.Add(new ContactModel() { ContactID = 4, ContactName = "Group 2", ContactNumber = "Doctor3,Nurse1" });
                _groupsCol.Add(new ContactModel() { ContactID = 4, ContactName = "My Group 2", ContactNumber = "Doctor3,Nurse1" });
                _groupsCol.Add(new ContactModel() { ContactID = 4, ContactName = "My Group 1", ContactNumber = "Doctor3,Nurse1" });
                _groupsCol.Add(new ContactModel() { ContactID = 4, ContactName = "Our team", ContactNumber = "Doctor3,Nurse1" });*/
                _groupsCol.Clear();
                List<ChatGroup> result = await App.ServiceManager.GetChatGroups();
                foreach (ChatGroup g in result)
                {
                    _groupsCol.Add(new ContactModel() { ContactID =g.Id, ContactName = g.Name, ContactNumber = string.Format("Number {0}",g.Name) });
                }
                //  gm.Add(contactCollection1);
                //  gm.Add(contactCollection2);
            }
            catch (Exception)
            {
                throw;
            }
            //return gm;
        }

        /// <summary>
        /// Get Contacts List with groups
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactGroupModel>> GetGroupsAsync(string searchStr)
        {
            try
            {
                gm = null;
                gm = new ObservableCollection<ContactGroupModel>();

                if (!string.IsNullOrWhiteSpace(searchStr))
                {
                    _groupsColFiltered = _groupsCol.Where(o => o.ContactName.ToLower().StartsWith(searchStr?.ToLower())).ToList();
                }
                else
                {
                    _groupsColFiltered = _groupsCol;
                }
                foreach (var item in _groupsColFiltered)
                {
                    var firstLetter = item.ContactName.Substring(0, 1);

                    if (gm.Any(x => x.ContactShortTitle == firstLetter.ToUpper()))
                    {
                        continue;
                    }

                    var collection = new ContactGroupModel
                    {
                        ContactTitle = $"{firstLetter.ToUpper()}",
                        ContactShortTitle = firstLetter.ToUpper(),
                        Image = string.Format("{0}.png", firstLetter.ToUpper()) //"B32.png"
                    };

                    var filteredItems = _groupsCol.Where(x => x.ContactName.StartsWith(firstLetter)).Select(x => new ContactModel
                    {
                        ContactID = x.ContactID,
                        ContactName = x.ContactName,
                        ContactNumber = x.ContactNumber,
                        FirstLetter = firstLetter,
                        Image = x.Image, // should have a byte array - buddhima 2018-03-31
                        Parameters = new ParamObject
                        {
                            ContactId = x.ContactID,
                            GroupId = (x.GroupId != 0) ? x.GroupId : 0,
                            UserName = x.ContactName // not sure
                        }

                    });

                    foreach (var fitem in filteredItems)
                    {
                        collection.Add(fitem);
                    }

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
                // contactGroupsVm.IsBusy = false;
            }
            return gm;
        }

        /// <summary>
        /// Show options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTabAddBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                imgTeamTabAddBtn.IsVisible = false;
                grdTeamTabReadyToCreateTeams.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Show search contacts window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_SearchbarBoxView_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Load only patients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeamTbPatients_Clicked(object sender, EventArgs e)
        {
            // ChangeTeamTabPatientAllColor(btnTeamTbPatients);
        }

        /// <summary>
        /// Load all teams
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeamTbAll_Clicked(object sender, EventArgs e)
        {
            try
            {
                // ChangeTeamTabPatientAllColor(btnTeamTbAll);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Change Selected Menu color
        /// </summary>
        /// <param name="btn"></param>
        private void ChangeTeamTabPatientAllColor(Button btn)
        {
            try
            {
                if (btn.BackgroundColor == Color.White)
                {
                    /*if (btn == btnTeamTbAll)
                    {
                        btnTeamTbAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                       // btnTeamTbPatients.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
                    else
                    {
                       // btnTeamTbPatients.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                       // btnTeamTbAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }*/
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Navigate to Message page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tgrTabTeam_TeamImage_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagePage());
        }

        /// <summary>
        /// Navigate to Message page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gr_grdlistViewTeamList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagePage());
        }

        #region Options list

        /// <summary>
        /// Open new chat page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_LblNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                Navigation.PushAsync(new NewConversationPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open new chat page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_ImgNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                Navigation.PushAsync(new NewConversationPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Cancel additional dialague
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_ImgCancel_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Create Team ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_LblCreateTeam_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTbTeamTypeSelect.IsVisible = true;
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        /// <summary>
        /// Create Team ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_ImgCreateTeam_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTbTeamTypeSelect.IsVisible = true;
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_LblCreateCategory_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                //grdTeamTbCategoryCreate.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_ImgCreateCategory_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                //grdTeamTbCategoryCreate.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        /// <summary>
        /// Open Modify Category Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_LblModifyCategory_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                Navigation.PushAsync(new CategoriesPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        /// <summary>
        /// Open Modify Category Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grTeamTab_ImgModifyCategoryp_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdTeamTabReadyToCreateTeams.IsVisible = false;
                imgTeamTabAddBtn.IsVisible = true;
                Navigation.PushAsync(new CategoriesPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        #endregion

        #region Team Type select ...

        private void btnTeamTbTeamTypeRegTeam_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTbTeamTypeSelect.IsVisible = false;
                grdTeamTb_GroupCreate.IsVisible = true;
                lblTeamTb_TeamCreateHeading.Text = "Team Name";
                vm.NewGroupModel.GroupType = "R";

                txtTeamTb_PatientTeamFname.IsVisible = false;
                txtTeamTb_PatientTeamLname.IsVisible = false;
                txtTeamTb_RegTeamName.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void btnTeamTbTeamTypePatientColTeam_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTbTeamTypeSelect.IsVisible = false;
                grdTeamTb_GroupCreate.IsVisible = true;
                lblTeamTb_TeamCreateHeading.Text = "Patient's Name";
                vm.NewGroupModel.GroupType = "P";

                txtTeamTb_PatientTeamFname.IsVisible = true;
                txtTeamTb_PatientTeamLname.IsVisible = true;
                txtTeamTb_RegTeamName.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void btnTeamTbTeamTypeCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTbTeamTypeSelect.IsVisible = false;

                vm.NewGroupModel = null;
                vm.NewGroupModel = new NewGroupViewModel();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        #endregion

        #region Team Create/Info ...

        private void btnTeamTb_TeamInfoConform_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (vm.NewGroupModel!=null)
                {
                }

                grdTeamTb_GroupCreate.IsVisible = false;
                Navigation.PushAsync(new TeamInfoPage(vm.NewGroupModel.GroupId));
                //  grdTeamTbTeamDatePicker.IsVisible = true;
                //grdTeamTbConformInfo.IsVisible = true;
                //tbTeam_CategoryPicker.ItemsSource = GetCategoryList();
                //tbTeam_CategoryPicker.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void btnTeamTb_TeamInfoCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTb_GroupCreate.IsVisible = false;
                vm.NewGroupModel = null;
                vm.NewGroupModel = new NewGroupViewModel();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        #endregion

        #region Team - patient DOB select ...
        //private void btnTeamTb_PatientDOBSelect_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        grdTeamTbConformInfo.IsVisible = true;
        //        tbTeam_CategoryPicker.ItemsSource = GetCategoryList();
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}
        #endregion

        #region Team Info conform ...
        /*
        public List<string> GetCategoryList()
        {
            List<string> collection = new List<string>();
            try
            {
                collection.Add("Category 1");
                collection.Add("Category 2");
                collection.Add("Category 3");
                collection.Add("Category 4");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            return collection;
        }

        private void btnTeamTb_ConformTeamInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                //grdTeamTbConformInfo.IsVisible = false;
                Navigation.PushAsync(new TeamInfoPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void btnTeamTb_CancelTeamInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                //grdTeamTbConformInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        */
        #endregion

        #region Category
        /*
        private void btnTeamTbCategoryCreateCancel_Clicked(object sender, EventArgs e)
        {
           // grdTeamTbCategoryCreate.IsVisible = false;
        }

        private void btnTeamTbCategoryCreateSave_Clicked(object sender, EventArgs e)
        {
            //grdTeamTbCategoryCreate.IsVisible = false;
        }
        */

        #endregion

        #endregion

        private void TxtSearchStr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void tgrSearchImage_Tapped(object sender, EventArgs e)
        {
            //listViewTeamList.ItemsSource
            vm.ContactGroups = await GetGroupsAsync(TxtSearchStr.Text.Trim());
        }
    }
}