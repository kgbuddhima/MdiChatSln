using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdiChat.Model;
using System.Collections.ObjectModel;
using System.IO;
using MdiChat.MdiWebService.DTO;
using MdiChat.Services;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuTabbedPage : TabbedPage
    {
        List<MdiContact> _allContacts = new List<MdiContact>();
        List<MdiContact> _contactsCopy1 = new List<MdiContact>();
        List<MdiContact> _contactsCopy2 = new List<MdiContact>();
        private ObservableCollection<ContactGroupModel> gm = null;

        public  MainMenuTabbedPage(bool fromNotification = false, int contactId = 1, int groupId = 0, string userName = "")
        {
            try

            {
                InitializeComponent();
                ListenForChatNotifications();

                if (fromNotification == true)
                 Navigation.PushAsync(new MessagePage(contactId, groupId, userName));

                NavigationPage.SetHasNavigationBar(this, false);
                lblOption1.Text = "1. If any of your device contacts are already using MDi Chat contacts page, tap on the contact icon (4th icon on the Top) to see them.";
                lblOption2.Text = "2. To add new contacts enter their names in serch boc on the top. Select the contact, tap on the menu icon at the upper left and select \"Add contacts\".";
                grdLogoutAlert.IsVisible = false;
                grdQuickStartGuide.IsVisible = true; /////

                // BindClasses();

                GetUser();

                grdReadyToMessage.IsVisible = false;
                //   BindContacts();

                grdMpReadyToCreateMessage.IsVisible = false;
                //   BindMessages();

                grdTeamTabReadyToCreateTeams.IsVisible = false;
                grdTeamTbTeamTypeSelect.IsVisible = false;
                grdTeamTb_GroupCreate.IsVisible = false;
                grdTeamTbCategoryCreate.IsVisible = false;
                //tbTeam_CategoryPicker.ItemsSource = GetCategoryList();
                grdTeamTbConformInfo.IsVisible = false;
                // grdTeamTbTeamDatePicker.IsVisible = false;
                // BindTeams();

                //  ChatClientFactory.GetChatClient().RaiseCustomEvent += HandleCustomEvent;
            }
            catch (Exception ex)
            {

                 DisplayAlert("Failed!..", ex.Message, "OK");
            }
        }
        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, ChatEventArgs e)
        {
            try
            {

                //Device.BeginInvokeOnMainThread(() =>
                //{
                //     DisplayAlert("Done!..", e.Message, "OK");
                //});
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //Console.WriteLine(id + " received this message: {0}", e.Message);
        }

        #region Settings menu items Tap Options

        private void lblUpdatePassword_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ChangePasswordPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblPermissionCode_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new PermissionCodeOnOffPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblMessageLifeTime_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MessageLifetimePage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblAbout_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new AboutPage());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void lblLegalInfo_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new LegalInfoPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblHelpAndSupport_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new HelpAndSupportPage());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open google play with app when Tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblrateMDiChat_Tapped(object sender, EventArgs e)
        {
            RateApp();
        }

        /// <summary>
        /// Open google play with app
        /// </summary>
        public void RateApp()
        {/*
            string appPackageName = Android.App.Application.Context.PackageName;

            try
            {
                var intent = new Android.Content.Intent(Android.Content.Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + appPackageName));
                // we need to add this, because the activity is in a new context.
                // Otherwise the runtime will block the execution and throw an exception
                intent.AddFlags(Android.Content.ActivityFlags.NewTask);

                Android.App.Application.Context.StartActivity(intent);
            }
            catch (Android.Content.ActivityNotFoundException)
            {
                var intent = new Android.Content.Intent(Android.Content.Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + appPackageName));
                // we need to add this, because the activity is in a new context.
                // Otherwise the runtime will block the execution and throw an exception
                intent.AddFlags(Android.Content.ActivityFlags.NewTask);

                Android.App.Application.Context.StartActivity(intent);
            }*/
        }

        /// <summary>
        /// Show logout alert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lblLogout_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool logout = await DisplayAlert("Logout from MDi Chat", "Are uou sure you want to logout ?", "Logout", "Cancel");
                if (logout)
                {
                    grdLogoutAlert.IsVisible = true;
                    loggingoutIndicator.IsVisible = true;
                    Helpers.Settings.User = null;

                    await Navigation.PushAsync(new LoginPage());
                }
                else grdLogoutAlert.IsVisible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Show logout process nd logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grLogoutWhiteBox_Tapped(object sender, EventArgs e)
        {
            loggingoutIndicator.IsVisible = false;
            grdLogoutAlert.IsVisible = false;
        }

        /// <summary>
        /// Open Profile page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tgrTbSettingEditUserInfo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        #region Quick start guide Tap options

        private void grSettingsTabStartupGuide_Tapped(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grSettingsTabSGbtnOK_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdQuickStartGuide.IsVisible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grSettingsTabSGbtnGoToCOnacts_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdQuickStartGuide.IsVisible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #endregion

        #region Class Tab

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private async void BindClasses()
        {
            listViewCategoryList.ItemsSource = await GetClasses();
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactModel>> GetClasses()
        {
            ObservableCollection<ContactModel> collection = new ObservableCollection<ContactModel>();
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

        #endregion

        #region Team Tab ...

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private async void BindTeams()
        {
            listViewTeamList.ItemsSource = await GetTeams();
        }

        /// <summary>
        /// Get Contacts List with groups
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactGroupModel>> GetTeams()
        {
            ObservableCollection<ContactGroupModel> gm = new ObservableCollection<ContactGroupModel>();
            ContactGroupModel contactCollection1 = new ContactGroupModel() { ContactTitle = "Class ward A", ContactShortTitle = "1 Team", Image = "B32.png" };
            ContactGroupModel contactCollection2 = new ContactGroupModel() { ContactTitle = "Class ward B", ContactShortTitle = "3 Teams", Image = "J32.png" };
            try
            {
                contactCollection1.Add(new ContactModel() { ContactID = 1, ContactName = "Team 1", ContactNumber = "Buddhima,Buddhika", FirstLetter = "T" });
                contactCollection2.Add(new ContactModel() { ContactID = 2, ContactName = "Team 2", ContactNumber = "Doctor1,Nurse1", FirstLetter = "T" });
                contactCollection2.Add(new ContactModel() { ContactID = 3, ContactName = "Team 3", ContactNumber = "Doctor2,Nurse1,Buddhima", FirstLetter = "T" });
                contactCollection2.Add(new ContactModel() { ContactID = 4, ContactName = "Team 4", ContactNumber = "Doctor3,Nurse1", FirstLetter = "T" });
                gm.Add(contactCollection1);
                gm.Add(contactCollection2);
            }
            catch (Exception)
            {
                throw;
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
            ChangeTeamTabPatientAllColor(btnTeamTbPatients);
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
                ChangeTeamTabPatientAllColor(btnTeamTbAll);
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
                    if (btn == btnTeamTbAll)
                    {
                        btnTeamTbAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                        btnTeamTbPatients.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
                    else
                    {
                        btnTeamTbPatients.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleRegular");
                        btnTeamTbAll.SetDynamicResource(VisualElement.StyleProperty, "buttonStyleTabUnSelected");
                    }
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
                grdTeamTbCategoryCreate.IsVisible = true;
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
                grdTeamTbCategoryCreate.IsVisible = true;
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
                grdTeamTb_GroupCreate.IsVisible = false;
                //  grdTeamTbTeamDatePicker.IsVisible = true;
                grdTeamTbConformInfo.IsVisible = true;
                tbTeam_CategoryPicker.ItemsSource = GetCategoryList();
                tbTeam_CategoryPicker.SelectedIndex = -1;
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
                grdTeamTbConformInfo.IsVisible = false;
                Navigation.PushAsync(new TeamInfoPage(0));
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
                grdTeamTbConformInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        #endregion

        #region Category

        private void btnTeamTbCategoryCreateCancel_Clicked(object sender, EventArgs e)
        {
            grdTeamTbCategoryCreate.IsVisible = false;
        }

        private void btnTeamTbCategoryCreateSave_Clicked(object sender, EventArgs e)
        {
            grdTeamTbCategoryCreate.IsVisible = false;
        }


        #endregion

        #endregion

        #region Contacts

        /// <summary>
        /// Bind Contacts to contact list
        /// </summary>
        private async void BindContacts()
        {
            listViewContacts.ItemsSource = await GetContacts(String.Empty);
            //   var result = await App.ServiceManager.SearchContacts(TxtSearchStr.Text);

            // listViewContacts.ItemsSource = result;
        }

        /// <summary>
        /// Get Contacts List with groups
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactGroupModel>> GetContacts(string searchStr)
        {

            if (!string.IsNullOrEmpty(searchStr))
            {
                _allContacts = await App.ServiceManager.SearchContacts(searchStr);
            }
            if (_allContacts == null || _allContacts.Count == 0)
            {
                return null;
            }

            //if (gm != null)
            //{
            //    return new ObservableCollection<ContactGroupModel>(gm.Where(x => x.ContactTitle.Contains(searchStr)));
            //}

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



            //ContactGroupModel contactCollection1 = new ContactGroupModel() { ContactTitle = "Start with B", ContactShortTitle = "B", Image = "B32.png" };
            //ContactGroupModel contactCollection2 = new ContactGroupModel() { ContactTitle = "Start with J", ContactShortTitle = "J", Image = "J32.png" };
            //try
            //{
            //    contactCollection1.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima 1", ContactNumber = "0713775822", FirstLetter = "B" });
            //    contactCollection1.Add(new ContactModel() { ContactID = 2, ContactName = "Buddhika", ContactNumber = "0713775822", FirstLetter = "B" });
            //    contactCollection2.Add(new ContactModel() { ContactID = 3, ContactName = "John Doe", ContactNumber = "0773093431", FirstLetter = "J" });
            //    contactCollection2.Add(new ContactModel() { ContactID = 4, ContactName = "Jeevan", ContactNumber = "0714180595", FirstLetter = "J" });
            //    gm.Add(contactCollection1);
            //    gm.Add(contactCollection2);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
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

        #endregion

        #region Message Tab

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private async void BindMessages()
        {
            listViewMsgList.ItemsSource = await GetMessages();
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ContactModel>> GetMessages()
        {
            ObservableCollection<ContactModel> collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Buddhima 1", ContactNumber = "Sample Message 1, text text", FirstLetter = "B" });
                collection.Add(new ContactModel() { ContactID = 2, ContactName = "Buddhika", ContactNumber = "Sample Message 2, text text", FirstLetter = "B" });
                collection.Add(new ContactModel() { ContactID = 3, ContactName = "John Doe", ContactNumber = "Sample Message 3, text text", FirstLetter = "J" });
                collection.Add(new ContactModel() { ContactID = 4, ContactName = "Jeevan", ContactNumber = "Sample Message 4, text text", FirstLetter = "J" });
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }

        /// <summary>
        /// Hide add button and show message options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTabAddBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                imgMsgTabAddBtn.IsVisible = false;
                grdMpReadyToCreateMessage.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Close message box options view and show add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_ImgCancel_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Close message box options view and show add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_BoxView_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open new messages page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_LblNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                Navigation.PushAsync(new NewConversationPage());
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open new messages page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_ImgNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                Navigation.PushAsync(new NewConversationPage());
                imgMsgTabAddBtn.IsVisible = true;
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
        private void grMsgTab_SearchbarBoxView_Tapped(object sender, EventArgs e)
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
        /// Navigate to Message page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gr_grdlistViewMsgList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagePage());
        }

        #endregion

        private async void GetUser()
        {
            try
            {
                var user = await App.ServiceManager.GetUSer();
                if (user == null)
                {
                    await Navigation.PushAsync(new LoginPage());
                }
                Helpers.Settings.User = user;
                if (user?.UserImage != null && user.UserImage.Length > 0)
                {
                    ImgUserImage.Source = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
                }
                if (user != null)
                {
                    lblUserName.Text = user.FirstName + " " + user.LastName;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //  public Timer timer;

        private async void TxtSearchStr_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            listViewContacts.ItemsSource = await GetContacts(TxtSearchStr.Text);
        }

        private void ListenForChatNotifications()
        {
            ChatClientFactory.GetChatClient().RaiseCustomEvent += HandleNotificationRecievedEvent;
        }
        void HandleNotificationRecievedEvent(object sender, ChatEventArgs e)
        {
            try
            {
                              
                Device.BeginInvokeOnMainThread(() =>
                {
                    var data = new NotificationData
                    {
                        Message = e.Message,
                        ContactId = e.ContactId,
                        UserName = e.UserName,
                        GroupId = e.GroupId
                    };
                    DependencyService.Get<ILocalNotificationService>()
                    .Show(data);
                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected override void OnDisappearing()
        {
            ChatClientFactory.GetChatClient().RaiseCustomEvent -= HandleNotificationRecievedEvent;
            base.OnDisappearing();
        }
    }
}
