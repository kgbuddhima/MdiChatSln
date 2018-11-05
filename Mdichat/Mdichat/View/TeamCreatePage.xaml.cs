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
    public partial class TeamCreatePage : ContentPage
    {
        public TeamCreatePage()
        {
            InitializeComponent();
           // BindingContext = new ContentPageViewModel();
        }

        #region Team Create Dialague

        private void btnTeamTb_TeamType_RegTeam_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTb_TeamTypeSelect.IsVisible = false;
              //  grdTeamTb_GroupCreate.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void btnTeamTb_TeamType_PatientColTeam_Clicked(object sender, EventArgs e)
        {
            try
            {
                grdTeamTb_TeamTypeSelect.IsVisible = false;
               // grdTeamTb_GroupCreate.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void btnTeamTb_TeamType_Cancel_Clicked(object sender, EventArgs e)
        {
            try
            {
               // grdTeamTb_TeamTypeSelect.IsVisible = false;
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
        }


        #endregion
    }

    /*
    class TeamCreatePageViewModel : INotifyPropertyChanged
    {

        public TeamCreatePageViewModel()
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

    }
    */
}
