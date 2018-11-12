//using Mdichat.MdiWebService;
using Xamarin.Forms;
using Mdichat.MdiWebService;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace Mdichat
{
    public partial class App : Application
    {
        public static MdiServiceManager ServiceManager { get; private set; }

        public App(string viewType = "login", int contactId = 0, int groupId = 0, string username = "", int chatId = 0)
        {
            InitializeComponent();
            MainPage = MainPage;
            
            ServiceManager = new MdiServiceManager(new RegistrationService(), new UserService(), new ChatService());
            if (viewType == "login")
                MainPage = MainPage; //new NavigationPage(new View.LoginPage());

            //if (viewType == "message")
            //    MainPage = new NavigationPage(new View.MessagePage(contactId, groupId, username, chatId));
            if (viewType == "main")
                MainPage = MainPage; //new NavigationPage(new View.MainPageTabbed(true, contactId, groupId, username, chatId));
        }



        protected override void OnStart()
        {
            // Handle when your app starts
          //  AppCenter.Start("android=c8e83cdc-b8f5-4dfb-961f-541e0f3bda62;",
           //     typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
    public static class ViewModelLocator
    {/*
        static ProfileInfoEditPageViewModel editProfileVM;

        public static ProfileInfoEditPageViewModel ProfileInfoEditPageViewModel
        {
            get
            {
                if (editProfileVM == null)
                {
                    editProfileVM = new ProfileInfoEditPageViewModel();
                }
                return editProfileVM;

            }
        }
        */

    }
}
