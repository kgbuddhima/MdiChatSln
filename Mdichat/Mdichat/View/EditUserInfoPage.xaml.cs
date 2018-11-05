using MdiChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MdiChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserInfoPage : ContentPage
    {
        public delegate void SetUserEventHandler(object source, EventArgs args);
        public event SetUserEventHandler UserSet;
        public ICommand NavigationCommand { get; }

        UserInfoViewModel _model;

        #region Constructor

        public EditUserInfoPage(UserInfoViewModel model)
        {
            InitializeComponent();
            _model = model;
            this.BindingContext = _model;
            NavigationCommand = new Command(NavigationCommandSave);
            ToolbarItems.Add(new ToolbarItem() { Text = "Save", Command = NavigationCommand });
        }

        #endregion

        #region Commands

        private async void NavigationCommandSave()
        {
          await Navigation.PopAsync(true);
        }

        #endregion

        #region private methods

        private void OnUserUpdated()
        {
            UserSet?.Invoke(_model, EventArgs.Empty);
        }

        #endregion
    }
}