using MdiChat.MdiWebService.DTO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using MdiChat.ViewModel;
using MdiChat.Helpers;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileInfoEditPage : ContentPage
    {
        public delegate void SetUserEventHandler(object source, EventArgs args);
        public event SetUserEventHandler UserSet;
        private MdiUser _user;
        //private ProfileInfoEditPageViewModel vm;
        private UserInfoViewModel vm= new UserInfoViewModel();
        public ICommand NavigationCommand { get; }

        #region constructor

        public ProfileInfoEditPage()
        {
            InitializeComponent();
            NavigationCommand = new Command(NavigationCommandToInfo);
            ToolbarItems.Add(new ToolbarItem() { Text = "Save", Command = NavigationCommand });
            //BindingContext = vm = new ProfileInfoEditPageViewModel();
            this.BindingContext = vm;

            BtnEditImage.Clicked += async (sender, args) =>
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
                        _user.UserImage = memoryStream.ToArray();
                    }
                    file.Dispose();
                    return stream;
                });
            };
            BtnCaptureImage.Clicked += async (sender, args) =>
            {
                await CaptureImage();
            };
            BtnSaveUserData.Clicked += async (sender, args) =>
            {
                await EditUser();
            };
        }

        #endregion

        #region protected override events

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //activityIndicatorIsBusy.IsVisible = false;
            GetUser();
        }

        #endregion

        #region Commands

        private async void NavigationCommandToInfo()
        {
            await EditUser(); // Navigation.PopAsync(true);
        }

        #endregion

        #region Methods

        private async Task CaptureImage()
        {
            try
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

                await DisplayAlert("File Location", file.Path, "OK");

                vm.Image = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStream().CopyTo(memoryStream);
                        _user.UserImage = memoryStream.ToArray();
                    }
                    file.Dispose();
                    return stream;
                });//ProfileImage.Source
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task EditUser()
        {
            try
            {
                vm.IsBusy = true;
                MdiResponse x = await App.ServiceManager.EditUserData(new UserRegister
                {
                    FirstName = TxtFirstName.Text,
                    LastName = TxtLastName.Text,
                    UserImage = _user.UserImage,
                    DeviceToken = Helpers.Settings.DeviceToken
                });
                if (x.IsSuccess)
                {
                    _user = await App.ServiceManager.GetUSer();
                    if (_user != null)
                    {
                        Helpers.Settings.User = _user;
                        vm.IsBusy = false;
                        OnUserUpdated(Helpers.Settings.User);
                        await Navigation.PopAsync(true);
                    }
                }
                else
                {
                    vm.IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnUserUpdated(MdiUser user)
        {
            UserSet?.Invoke(user,EventArgs.Empty);
        }

        private async void GetUser()
        {
            try
            {
                vm.IsBusy = true;
                _user = await App.ServiceManager.GetUSer();
                if (_user == null)
                {
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    UpdateViewFromUserData();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                vm.IsBusy = false;
            }

        }

        private void UpdateViewFromUserData()
        {
            try
            {
                if (_user.UserImage != null && _user.UserImage.Length > 0)
                {
                    // ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(_user.UserImage));
                    vm.Image = ImageSource.FromStream(() => new MemoryStream(_user.UserImage));
                    if (vm.Image == null)
                    {
                        vm.Image = ImgLib.DefaultUserImg;
                    }
                }
                // ProfileImage.Source = vm.Image;
                vm.FirstName = _user.FirstName;
                vm.LastName = _user.LastName;
                // TxtFirstName.Text = vm.FirstName;
                // TxtLastName.Text = vm.LastName;
            }
            catch (Exception)
            {
                throw;
            }

            // TxtFirstName.Text = _user.FirstName;
            // TxtLastName.Text = _user.LastName;

        }

        #endregion

        /// <summary>
        /// Navigate to  edit additional info page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdditionalData_Clicked(object sender, EventArgs e)
        {
            EditUserInfoPage editInfo = new EditUserInfoPage(vm);
            editInfo.UserSet += OnInfoSet; ;
            Navigation.PushAsync(editInfo);
        }

        private void OnInfoSet(object source, EventArgs args)
        {
            vm = (UserInfoViewModel)source;
            DisplayAlert("","","");
        }
    }



    public class ProfileInfoEditPageViewModel  : BindableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] UserImage { get; set; }
        public Xamarin.Forms.ImageSource Image { get; set; }
        public bool IsBusy { get; set; }

    }
}