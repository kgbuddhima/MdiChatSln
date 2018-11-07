using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mdichat.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mdichat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            NavigationCommand = new Command(NavigationCommandToInfo);
            ToolbarItems.Add(new ToolbarItem() { Icon = ImgLib.InforNavbarImg, Command = NavigationCommand });
        }

        public ICommand NavigationCommand { get; }

        void NavigationCommandToInfo() =>
            Navigation.PushAsync(new AcknolagementPage());
    }
}