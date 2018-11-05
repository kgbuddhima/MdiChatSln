using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdiChat.Helpers;
using MdiChat.Services;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TbSettingsPage : ContentPage
    {
        public bool IsFirstTimeLogin
        {
            get
            {
                return Settings.IsFirstTimeUser;
            }
            set
            {
                if (Settings.IsFirstTimeUser == value)
                    return;
                Settings.IsFirstTimeUser = value;
            }
        }
        bool navigetedFirst = false;

        #region Cnstructors

        public TbSettingsPage()
        {
            InitializeComponent();
            navigetedFirst = true;
        }

        #endregion

        #region protected override events

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                lblOption1.Text = "1. If any of your device contacts are already using MDi Chat contacts page, tap on the contact icon (4th icon on the Top) to see them.";
                lblOption2.Text = "2. To add new contacts enter their names in serch boc on the top. Select the contact, tap on the menu icon at the upper left and select \"Add contacts\".";
                grdLogoutAlert.IsVisible = false;
                QuickStartGuideVisibilityCheck();
                GetUser();                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                navigetedFirst = false;
            }
        }

        #endregion

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
                    //loggingoutIndicator.IsVisible = true;
                    Helpers.Settings.User = null;
                    //await Navigation.PopAsync(true);
                    var closer = DependencyService.Get<ICloseApplication>();
                    closer?.CloseApp();
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
            var profilePg = new ProfilePage();
            profilePg.UserSet += this.OnUserEdited;
            Navigation.PushAsync(profilePg);
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
                IsFirstTimeLogin = false;
                grdQuickStartGuide.IsVisible = IsFirstTimeLogin;
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

        #region private methods

        /// <summary>
        /// Load user data
        /// </summary>
        private async void GetUser()
        {
            try
            {
                MdiWebService.DTO.MdiUser user = null;
                if (navigetedFirst)
                {
                    user = await App.ServiceManager.GetUSer();
                    if (user == null)
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        Helpers.Settings.User = user;
                    }
                    if (user?.UserImage != null && user.UserImage.Length > 0)
                    {
                        ImgUserImage.Source = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
                    }
                    if (user != null)
                    {
                        lblUserName.Text = user.FirstName + " " + user.LastName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Show quick startup guide
        /// </summary>
        private void QuickStartGuideVisibilityCheck()
        {
            grdQuickStartGuide.IsVisible = IsFirstTimeLogin;
        }

        #endregion

        #region private events

        private void OnUserEdited(object source, EventArgs e)
        {
            try
            {
                MdiWebService.DTO.MdiUser user = (MdiWebService.DTO.MdiUser)source;
                if (user != null)
                {
                    if (user?.UserImage != null && user.UserImage.Length > 0)
                    {
                        ImgUserImage.Source = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
                    }
                    lblUserName.Text = user.FirstName + " " + user.LastName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}