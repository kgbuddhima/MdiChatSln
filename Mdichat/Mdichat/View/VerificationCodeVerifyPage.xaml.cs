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
    public partial class VerificationCodeVerifyPage : ContentPage
    {
        #region Private properties

        string _email;
        string _mobile;
        private bool _isMobileCode = false;

        #endregion

        #region Constructors

        public VerificationCodeVerifyPage(string email,string mobile, bool isMobile = false )
        {
            InitializeComponent();
            this.Title = "Enter Code";
            _email = email;
            _mobile = mobile;
            _isMobileCode = isMobile;

            // BindingContext = new VerificationCodeVerifyPageViewModel();
        }

        #endregion

        #region overrides

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txt1.Focus();
        }

        /// <summary>
        /// raise when back button pressed and delete if the user has not been registered
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            var payload = new RegisterContact
            {
                Email = _email,
                MobileNumber = _mobile
            };
            var response = App.ServiceManager.DeleteUnRegisteredUser(payload);
            ActivityIndicatorVisibility(false);
            return false;
        }

        #endregion

        #region Events

        /// <summary>
        /// Validate verification code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVerify_Clicked(object sender, EventArgs e)
        {
            try
            {
                string code = $"{txt1.Text}{txt2.Text}{txt3.Text}{txt4.Text}{txt5.Text}{txt6.Text}{txt7.Text}";
                if (string.IsNullOrEmpty(code))
                {
                    await DisplayAlert("Empty code", "Please insert the correct verification code !", "OK");
                    return;
                }
                if (code.Length < 7)
                {
                    await DisplayAlert("Incomplete code", "Please insert the 7 digit verification code !", "OK");
                    return;
                }

                ActivityIndicatorVisibility(true);
                var payload = new VerifyCode
                {
                    DeviceToken = Helpers.Settings.DeviceToken,
                    Code = code
                };
                var response = (_isMobileCode) ? await App.ServiceManager.ConfirmMobileNumber(payload) 
                    : await App.ServiceManager.ConfirmEmailAsync(_email, code);
                if (response)
                {
                    ActivityIndicatorVisibility(false);
                    await Navigation.PushAsync(new RegistrationPage(_mobile,_email));
                }
                else
                {
                    ActivityIndicatorVisibility(false);
                    await DisplayAlert("Verification failed. ", "Please insert the correct verification code !", "OK");
                }
                //await Navigation.PushAsync(new RegistrationPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {

            }
            else txt2.Focus();
        }

        private void txt2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt1.Focus();
            }
            else txt3.Focus();
        }

        private void txt3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt2.Focus();
            }
            else txt4.Focus();
        }

        private void txt4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt3.Focus();
            }
            else txt5.Focus();
        }

        private void txt5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt4.Focus();
            }
            else txt6.Focus();
        }

        private void txt6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt5.Focus();
            }
            else txt7.Focus();
        }

        private void txt7_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                txt6.Focus();
            }
            else
            {
                btnVerify.Focus();
                btnVerify_Clicked(sender,e);
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

        private bool IsTextDeleted(TextChangedEventArgs e)
        {
            int oldLen, newLen = 0;
            oldLen = e.OldTextValue?.Length > 0 ? e.OldTextValue.Length : 0;
            newLen = e.NewTextValue?.Length > 0 ? e.NewTextValue.Length : 0;
            return oldLen >= newLen;
        }

        #endregion
    }
}
