using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MdiChat.MdiWebService.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdiChat.Model;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationModeSelectionPage : ContentPage
    {
        #region Constructors

        public VerificationModeSelectionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // BindingContext = new VerificationModeSelectionPageViewModel();
        }

        #endregion

        #region Events

        /// <summary>
        /// Send verification code to mobile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVerifyPhone_ClickedAsync(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtCountryCode.Text) || string.IsNullOrEmpty(txtPhone.Text))
                {
                    await DisplayAlert("Conform",
                        "Please Enter valid mobile number.", "OK");
                }
                else
                {
                    var payload = new MobileNoRegistration
                    {
                        CountryCode = txtCountryCode.Text,
                        PhoneNumber = txtPhone.Text
                    };
                    var reponse = await App.ServiceManager.RequestRegisterMobileAsync(payload);
                    if (!string.IsNullOrEmpty(reponse)) Helpers.Settings.DeviceToken = reponse;
                    else await DisplayAlert("Failed..", "Mobile number registration filed", "OK");
                    ActivityIndicatorVisibility(false);
                    await Navigation.PushAsync(new VerificationCodeVerifyPage(string.Empty, payload.PhoneNumber, true));
                }
                // await Navigation.PushAsync(new VerificationCodeVerifyPage(txtPhone.Text));
            }
            catch (Exception ex)
            {
                ActivityIndicatorVisibility(false);
                await DisplayAlert("Unsuccess!", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Send verification email to email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVerifyEmail_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                bool result = await DisplayAlert("Conform", string.Format("Please Conform {0} is your actual email.", txtEmail.Text), "OK", "Edit");

               if (result)
                {
                    ActivityIndicatorVisibility(true);
                    string response = await App.ServiceManager.RequestRegisterEmailAsync(txtEmail.Text);
                    if (response.Equals(Constants.S_Exists) || response.Equals(Constants.S_NotSuccess))
                    {
                        ActivityIndicatorVisibility(false);
                        if (response == Constants.S_Exists)
                        {
                            await DisplayAlert("Failed!", Constants.Msg_Email_Exists, "OK");
                            await Navigation.PushAsync(new LoginPage());
                        }
                        else
                        {
                            await DisplayAlert("Failed!", Constants.Msg_Request_Failed, "OK");
                        }
                       
                    }
                    else
                    {
                        ActivityIndicatorVisibility(false);
                        if (!string.IsNullOrEmpty(response)) Helpers.Settings.DeviceToken = response;
                        await Navigation.PushAsync(new VerificationCodeVerifyPage(txtEmail.Text,null));

                    }
                }
                //await Navigation.PushAsync(new VerificationCodeVerifyPage(txtEmail.Text));
            }
            catch (Exception ex)
            {
                ActivityIndicatorVisibility(false);
                await DisplayAlert("Unsuccess!", ex.Message, "OK");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show hide acrivity indicator
        /// </summary>
        /// <param name="active"></param>
        private void ActivityIndicatorVisibility(bool active)
        {
            actSendingCode.IsVisible = active;
            actSendingCode.IsRunning = active;
        }

        #endregion
    }
}
