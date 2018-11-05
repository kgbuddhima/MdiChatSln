using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdiChat.ViewModel;
using MdiChat.Helpers;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public delegate void SetUserEventHandler(object source, EventArgs args);
        public event SetUserEventHandler UserSet;
        UserInfoViewModel vm = new UserInfoViewModel();

        #region constructors

        public ProfilePage()
        {
            InitializeComponent();
            //BindingContext = new ProfilePageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = vm;

        }

        #endregion

        #region protected override methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var user = Helpers.Settings.User;
                if (user.UserImage != null && user.UserImage.Length > 0)
                {
                    //ImgUserImage.Source = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
                    vm.Image = ImageSource.FromStream(() => new MemoryStream(user.UserImage));
                    if (vm.Image==null)
                    {
                        vm.Image = ImgLib.DefaultUserImg;
                    }
                }
                // LblUserName.Text = user.FirstName;
                vm.FullName = user.FirstName;
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            UserSet?.Invoke(Settings.User, EventArgs.Empty);
            return false;
        }

        #endregion

        #region events

        private void tgrBack_Tapped(object sender, EventArgs e)
        {
            UserSet?.Invoke(Settings.User,EventArgs.Empty);
            Navigation.PopAsync();
        }

        /// <summary>
        /// Navigate to Profile info edit page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tgrEditInfo_Tapped(object sender, EventArgs e)
        {
            var profilePage = new ProfileInfoEditPage();
            profilePage.UserSet += this.OnMemberSet;
            Navigation.PushAsync(profilePage);
        }

        private void OnMemberSet(object source, EventArgs e)
        {
            try
            {
                MdiWebService.DTO.MdiUser m = (MdiWebService.DTO.MdiUser)source;
                if (m != null)
                {
                    if (m.UserImage != null && m.UserImage.Length > 0)
                    {
                        vm.Image = ImageSource.FromStream(() => new MemoryStream(m.UserImage));
                        if (vm.Image == null)
                        {
                            vm.Image = ImgLib.DefaultUserImg;
                        }
                    }
                    vm.FullName = m.FirstName;
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