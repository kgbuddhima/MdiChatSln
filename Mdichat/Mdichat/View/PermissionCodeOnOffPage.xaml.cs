using MdiChat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermissionCodeOnOffPage : ContentPage
    {
        public PermissionCodeOnOffPage()
        {
            InitializeComponent();
            grdTimeoutList.IsVisible = false;
            BindingContext = new PermisionCodeVM();
        }

        private void btnUpdatePermissioCode_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new PermissionCodeUpdatePage());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTimeout_Clicked(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = true;
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
        }

        private void tgrQuickly_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "Very Quickly";
        }

        private void tgrOneM_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "1 Minute";
        }

        private void tgrFiveM_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "5 Minute";
        }

        private void tgrThirtyM_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "30 Minute";
        }

        private void tgrTenM_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "10 Minute";
        }

        private void tgrOneH_Tapped(object sender, EventArgs e)
        {
            grdTimeoutList.IsVisible = false;
            lblTimeoutPeriod.Text = "1 Hour";
        }
    }

   /* class PermissionCodeOnOffPageViewModel : INotifyPropertyChanged
    {

        public PermissionCodeOnOffPageViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }*/
}
