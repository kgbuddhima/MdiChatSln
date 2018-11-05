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

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConformPasswordPage : ContentPage
    {
        public ConformPasswordPage()
        {
            InitializeComponent();
            PkrCountryCode.Title = "(+94)";
            PkrCountryCode.VerticalOptions = LayoutOptions.CenterAndExpand;
            PkrCountryCode.ItemsSource = new List<string>{ "+94", "+60"};
          //  BindingContext = new ConformPasswordPageViewModel();
        }

        private async void BtnSendToPhone_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtMobileNumber.Text))
            {
                await DisplayAlert("Failed!..", "Please enter valid mobile number", "OK");
                return;
            }
            ActLoader.IsRunning = true;
            var response = await App.ServiceManager.ForgotPassword(new ForgotPassword
            {
                MobileNumber = PkrCountryCode.SelectedItem + TxtMobileNumber.Text,
                Email = null
            });

            if (response.IsSuccess)
            {
                Helpers.Settings.DeviceToken = response.DeviceToken;
                Helpers.Settings.PasswordResetCode = response.ResetCode;
                ActLoader.IsRunning = false;
                await DisplayAlert("Done!..", "Reset code has been sent to your mobile", "OK");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ActLoader.IsRunning = false;
                    await DisplayAlert("Failed!..", response.Message, "OK");
                    return;
                }
            }
            ActLoader.IsRunning = false;
            await DisplayAlert("Failed!..", "Something went wrong!..", "OK");
        }

        private async void BtnSendToEmail_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtEmail.Text))
            {
                await DisplayAlert("Failed!..", "Please enter valid email", "OK");
                return;
            }

            ActLoader.IsRunning = true;
            var response = await App.ServiceManager.ForgotPassword(new ForgotPassword
            {
                Email = TxtEmail.Text,
                MobileNumber = null
            });

            if (response.IsSuccess)
            {
                Helpers.Settings.DeviceToken = response.DeviceToken;
                Helpers.Settings.PasswordResetCode = response.ResetCode;

                ActLoader.IsRunning = false;
                await DisplayAlert("Done!..", "Reset code has been sent to your email", "OK");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ActLoader.IsRunning = false;
                    await DisplayAlert("Failed!..", response.Message, "OK");
                    return;
                }
            }
            ActLoader.IsRunning = false;
            await DisplayAlert("Failed!..", "Something went wrong!..", "OK");
        }

        private async void BtnResetPassword_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCode1.Text) || string.IsNullOrEmpty(TxtCode2.Text) ||
                string.IsNullOrEmpty(TxtCode3.Text) || string.IsNullOrEmpty(TxtCode4.Text))
            {
                await DisplayAlert("Failed!..", "Please enter valid reset code!..", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TxtPassword.Text) || string.IsNullOrEmpty(TxtConfirmPassword.Text))
            {
                await DisplayAlert("Failed!..", "Please enter valid passowrd!..", "OK");
                return;
            }
            if (TxtPassword.Text != TxtConfirmPassword.Text)
            {
                await DisplayAlert("Failed!..", "Passwrod mismatch!..", "OK");
                return;
            }

            ActLoader.IsRunning = true;

            var _code = Helpers.Settings.PasswordResetCode;

            var response = await App.ServiceManager.ResetPassword(new PasswordReset
            {
                Password = TxtPassword.Text,
                ConfirmPassword = TxtConfirmPassword.Text,
                Code = Helpers.Settings.PasswordResetCode,
                DeviceToken = Helpers.Settings.DeviceToken,
                MdiResetCode = $"{TxtCode1.Text}{TxtCode2.Text}{TxtCode3.Text}{TxtCode4.Text}"
            });

            if (response)
            {
                await DisplayAlert("Done!..", "Password resetting completed", "OK");
                ActLoader.IsRunning = false;
                await Navigation.PushAsync(new LoginPage());
                
                return;
                // redirect
            }
            ActLoader.IsRunning = false;
            await DisplayAlert("Failed!..", "Something went wrong!..", "OK");

        }

        private void TxtCode1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsTextDeleted(e))
            {
                TxtCode2.Focus();
            }
        }

        private void TxtCode2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                TxtCode1.Focus();
            }
            else
            {
                TxtCode3.Focus();
            }
        }

        private void TxtCode3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                TxtCode2.Focus();
            }
            else
            {
                TxtCode4.Focus();
            }            
        }

        private void TxtCode4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsTextDeleted(e))
            {
                TxtCode3.Focus();
            }
            else
            {
                BtnResetPassword.Focus();
            }
        }

        private bool IsTextDeleted(TextChangedEventArgs e)
        {
            int oldLen, newLen=0;
            oldLen = e.OldTextValue?.Length > 0 ? e.OldTextValue.Length : 0;
            newLen = e.NewTextValue?.Length > 0 ? e.NewTextValue.Length : 0;
            return oldLen >= newLen;
        }
    }
}
