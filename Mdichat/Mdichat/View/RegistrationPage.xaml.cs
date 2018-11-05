using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MdiChat.MdiWebService;
using MdiChat.MdiWebService.DTO;
using MdiChat.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        #region private methods

        private string ConfirmedEmail;
        private string ConfirmedMobile;
        bool _isRegistered = false;
        private byte[] _userImage;

        #endregion

        #region constructors

        public RegistrationPage(string mobile, string email)
        {
            InitializeComponent();
            ConfirmedEmail = email;
            ConfirmedMobile = mobile;
            grdSavingAlert.IsVisible = false;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                HandleImageSelection();
            };
            ProfileImage.GestureRecognizers.Add(tapGestureRecognizer);
            // BindingContext = new RegistrationPageViewModel();
        }

        #endregion

        #region override events

        /// <summary>
        /// raise when back button pressed and delete if the user has not been registered
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            try
            {
                base.OnBackButtonPressed();
                var payload = new RegisterContact
                {
                    Email = ConfirmedEmail,
                    MobileNumber = ConfirmedMobile
                };
                ActivityIndicatorVisibility(true);
                var response = App.ServiceManager.DeleteUnRegisteredUser(payload);
                ActivityIndicatorVisibility(false);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region events

        private async void btnRegister_ClickedAsync(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(TxtFirstName.Text) || string.IsNullOrEmpty(Password.Text))
                {
                    await DisplayAlert("Error", "Please fill required data..", "OK");
                    return;
                }

                if (!Helpers.Utility.IsValidPassword(Password.Text))
                {
                    await DisplayAlert("Invalid Password",
                        "Password must contain at least one Upper case, one symbol, one digit, one lowercase character and 8 digit length",
                        "OK");
                    return;
                }

                ActivityIndicatorVisibility(true);
                var payload = new UserRegister
                {
                    DeviceToken = Helpers.Settings.DeviceToken,
                    FirstName = TxtFirstName.Text,
                    LastName = TxtLastName.Text,
                    UserName = TxtUserName.Text,
                    Password = Password.Text,
                    UserImage = _userImage ?? new byte[0]
                };
                var response = await App.ServiceManager.UpdateUserData(payload);
                ActivityIndicatorVisibility(false);
                if (response.IsSuccess)
                {
                    _isRegistered = response.IsSuccess;
                    await Navigation.PushAsync(new LoginPage());
                }
                else await DisplayAlert("Error", response.Message, "OK");
            }
            catch (Exception ex)
            {
                ActivityIndicatorVisibility(false);
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Delete un registered user and Abroat the process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnCancel_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                var payload = new RegisterContact
                {
                    Email = ConfirmedEmail,
                    MobileNumber = ConfirmedMobile
                };
                ActivityIndicatorVisibility(true);
                var response = await App.ServiceManager.DeleteUnRegisteredUser(payload);
                ActivityIndicatorVisibility(false);
                var closer = DependencyService.Get<ICloseApplication>();
                closer?.CloseApp();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void grWhiteBox_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdSavingAlert.IsVisible = false;
                Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }

        }

        #endregion

        #region methods

        private void ActivityIndicatorVisibility(bool active)
        {
            ActSendingCode.IsVisible = active;
            ActSendingCode.IsRunning = active;
        }

        private async void HandleImageSelection()
        {
            try
            {
                var imageSelectionOption =
                    await DisplayActionSheet("Upload Photo", "Not now", null, "Camera", "Gallary");
                switch (imageSelectionOption)
                {
                    case "Gallary":
                        {
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                                return;
                            }
                            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                            {
                                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                            });


                            if (file == null)
                                return;

                            ProfileImage.Source = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    _userImage = memoryStream.ToArray();
                                }
                                file.Dispose();
                                return stream;
                            });
                        }
                        break;
                    case "Camera":
                        {

                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                            {
                                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                                return;
                            }

                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Test",
                                SaveToAlbum = true,
                                CompressionQuality = 75,
                                CustomPhotoSize = 50,
                                PhotoSize = PhotoSize.MaxWidthHeight,
                                MaxWidthHeight = 2000
                            });

                            if (file == null)
                                return;

                            // DisplayAlert("File Location", file.Path, "OK");

                            ProfileImage.Source = ImageSource.FromStream(() =>
                            {
                                var stream = file.GetStream();
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    _userImage = memoryStream.ToArray();
                                }
                                file.Dispose();
                                return stream;
                            });
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
    }
}
