
using Android.App;
using Mdichat.Droid.Services;
using Mdichat.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace Mdichat.Droid.Services
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}