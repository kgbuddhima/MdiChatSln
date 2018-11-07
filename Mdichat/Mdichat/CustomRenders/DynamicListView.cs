using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mdichat
{
    public class DynamicListView : ListView
    {
        public DynamicListView()
        {
            this.BindingContextChanged += (sender, e) =>
            {
                this.InvalidateMeasure();
            };
        }
    }
}
