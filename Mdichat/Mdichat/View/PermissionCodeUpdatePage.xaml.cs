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
using MdiChat.Helpers;
using MdiChat.Model;

namespace MdiChat.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermissionCodeUpdatePage : ContentPage
    {
        public PermissionCodeUpdatePage()
        {
            InitializeComponent();
          //  BindingContext = new ContentPageViewModel();
        }

        #region Private methods ...

        /// <summary>
        /// Clear permission code fields
        /// </summary>
        private void ClearFields()
        {
            txt1.Text = string.Empty;
            txt2.Text = string.Empty;
            txt3.Text = string.Empty;
            txt4.Text = string.Empty;
            txt1.Focus();
        }

        /// <summary>
        /// Fill code fields and verify permission code
        /// </summary>
        /// <param name="btn"></param>
        private void EnterCodeToText(Button btn)
        {
            if (string.IsNullOrWhiteSpace(txt1.Text))
            {
                txt1.Text = btn.Text;
            }
            else if (string.IsNullOrWhiteSpace(txt2.Text))
            {
                txt2.Text = btn.Text;
            }
            else if (string.IsNullOrWhiteSpace(txt3.Text))
            {
                txt3.Text = btn.Text;
            }
            else if (string.IsNullOrWhiteSpace(txt4.Text))
            {
                txt4.Text = btn.Text;
            }
        }

        /// <summary>
        /// verify permission code and pop to root
        /// </summary>
        private void VerifyCodeAndPop()
        {
            try
            {
                if (
                !string.IsNullOrWhiteSpace(txt1.Text) &&
                !string.IsNullOrWhiteSpace(txt2.Text) &&
                !string.IsNullOrWhiteSpace(txt3.Text) &&
                !string.IsNullOrWhiteSpace(txt4.Text))
                {
                    string code = string.Format("{0}{1}{2}{3}", txt1.Text, txt2.Text, txt3.Text, txt4.Text);
                    Settings.PermissionCode = code;
                    Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert(Constants.S_Error, Constants.Msg_PermissionCode_worng, Constants.S_OK);
                ClearFields();
            }

        }

        #endregion

        #region Events

        private void btn1_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn1);
        }

        private void btn2_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn2);
        }

        private void btn3_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn3);
        }

        private void btn4_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn4);
        }

        private void btn5_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn5);
        }

        private void btn6_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn6);
        }

        private void btn7_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn7);
        }

        private void btn8_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn8);
        }

        private void btn9_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn9);
        }

        private void btn0_Clicked(object sender, EventArgs e)
        {
            EnterCodeToText(btn0);
        }

        private void txt4_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerifyCodeAndPop();
        }

        #endregion

    }
}
