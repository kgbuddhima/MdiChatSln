using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MdiChat.MdiWebService.DTO;
using MdiChat.Services;
using MdiChat.Validations;
//using MdiChat.View;
using Xamarin.Forms;

namespace MdiChat.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        
        private bool _isValid;
        private bool _isLogin;
        private string _authUrl;


        public LoginViewModel()
        {
           

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

    

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        public string LoginUrl
        {
            get
            {
                return _authUrl;
            }
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => LoginUrl);
            }
        }

        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPassword());

        public ICommand SignInCommand => new Command(async () => await SignInAsync());

        public ICommand RegisterCommand => new Command(async () => await Register());

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

      
    

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private async Task SignInAsync()
        {
            if (!Validate() || IsLogin) return;

            IsBusy = true;
            IsLogin = true;


            try
            {
                var response = await App.ServiceManager.Login(new UserLogin
                {
                    UserName = UserName.Value,
                    Password = Password.Value
                });

                if (response.IsSuccess)
                {


                    Helpers.Settings.AuthToken = response.AuthToken;
                    //Task.Run(() =>
                    //{
                    //    DependencyService.Get<ISignalRClient>(DependencyFetchTarget.GlobalInstance)
                    //        .ConnectToServer(Helpers.Settings.AuthToken);
                    //});
                  

                    await App.ServiceManager.RegisterNotificationHub(new NotificationHubRegistration()
                    {
                        Token = Helpers.Settings.FirebaseToken,
                        DeviceType = "gcm",
                        NotificationRegistrationId = Helpers.Settings.NotificationRegistrationId
                    });
                   // await Navigation.PushAsync(new MainPageTabbed());
                }
                else
                {
                    MessagingCenter.Send(response.Message, "MessageAlert");
                }

                IsBusy = false;
                IsLogin = false;
            }
            catch (Exception ex)
            {

               Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
                IsLogin = false;
            }
        }

        private async Task Register()
        {
          //await  Navigation.PushAsync(new VerificationModeSelectionPage());
        }

        private async Task ForgotPassword()
        {
           //await Navigation.PushAsync(new ConformPasswordPage());
        }
    }

}
