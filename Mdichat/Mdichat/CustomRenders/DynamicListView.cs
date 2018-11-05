using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MdiChat
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
