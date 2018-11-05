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
	public partial class TbClassesPage : ContentPage
	{
		public TbClassesPage ()
		{
			InitializeComponent ();
            Task.Run(BindClassesAsync);
        }

        /// <summary>
        /// Bind Messages to Message List
        /// </summary>
        private async Task BindClassesAsync()
        {
            listViewCategoryList.ItemsSource = await GetClassesAsync();
        }

        /// <summary>
        /// Get Messages to Message List
        /// </summary>
        /// <returns></returns>
        private async Task<System.Collections.ObjectModel.ObservableCollection<ContactModel>> GetClassesAsync()
        {
            ObservableCollection<ContactModel> collection = new ObservableCollection<ContactModel>();
            try
            {
                collection.Add(new ContactModel() { ContactID = 1, ContactName = "Class ward 1", ContactNumber = "0 Teams" });
                collection.Add(new ContactModel() { ContactID = 2, ContactName = "Class ward 2", ContactNumber = "2 Teams" });
                collection.Add(new ContactModel() { ContactID = 3, ContactName = "Class ward 3", ContactNumber = "1 Team" });
                collection.Add(new ContactModel() { ContactID = 4, ContactName = "Class ward 4", ContactNumber = "3 Teams" });
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }
    }
}