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
    public partial class TeamInfoChangeDescPage : ContentPage
    {
        public delegate void DescriptionChangeEventHandler(object source, EventArgs args);
        public event DescriptionChangeEventHandler DescriptionChange;
        string _description;

        public TeamInfoChangeDescPage(string dsc)
        {
            InitializeComponent();
            _description = dsc;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtDsc.Text = _description;
        }

        private void btnSaveTeamDesc_Clicked(object sender, EventArgs e)
        {
            OnDescriptionChanged(txtDsc.Text);
            Navigation.PopAsync();
        }

        protected virtual void OnDescriptionChanged(string dsc)
        {
            DescriptionChange?.Invoke(dsc, EventArgs.Empty);
        }
    }

}