using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MdiChat.MdiWebService.DTO;
using MdiChat.Services;
using MdiChat.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _vm;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _vm = new LoginViewModel
            {
                Navigation = Navigation
            };
            BindingContext = _vm;
            
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TDOD add Constant
            MessagingCenter.Subscribe<string>(this, "MessageAlert", (value) =>
            {
                DisplayAlert("Failed!..", value, "OK");
            });
        }


       
    }

   
}
