using MdiChat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MdiChat.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TbMessagesPage : ContentPage
	{
        public TbMessagesPage ()
		{
			InitializeComponent ();
            grdMpReadyToCreateMessage.IsVisible = false;
           
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        MessagingCenter.Subscribe<MessageInfoModel>(this, "IncommingMesssage", (IncommingMesssage) =>
	        {
	            MessageInfoModel p = new MessageInfoModel()
	            {
	                ContactId = IncommingMesssage.ContactId,
	                GroupId = IncommingMesssage.GroupId,
	                Username = IncommingMesssage.Username,
	                ChatId = IncommingMesssage.ChatId
	            };
	            Navigation.PushAsync(new MessagePage(p.ContactId, p.GroupId, p.Username, p.ChatId));
	        });

        }

	    #region Message Tab

       

        /// <summary>
        /// Hide add button and show message options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTabAddBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                imgMsgTabAddBtn.IsVisible = false;
                grdMpReadyToCreateMessage.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Close message box options view and show add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_ImgCancel_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Close message box options view and show add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_BoxView_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open new messages page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_LblNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                Navigation.PushAsync(new NewConversationPage());
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open new messages page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTb_ImgNewMsg_Tapped(object sender, EventArgs e)
        {
            try
            {
                grdMpReadyToCreateMessage.IsVisible = false;
                Navigation.PushAsync(new NewConversationPage());
                imgMsgTabAddBtn.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Open Search contacts page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grMsgTab_SearchbarBoxView_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new SearchContactsPage());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Navigate to Message page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gr_grdlistViewMsgList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagePage());
        }

        #endregion
    }
}