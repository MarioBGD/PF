using PF.Common.Currencies;
using PF.Mobile.App.DAL;
using PF.Mobile.App.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App
{
    public partial class App : Application
    {
        private static App _instance;
        public static bool IsLoginPage { get; set; }
        public static long CurrentGroupId { get; set; } = 0;
        public static DTO.Groups.GroupDTO CurrentGroup { get; set; }

        public App()
        {
            _instance = this;
            Currencies.InitCurrencies();
            DAL.Api.ApiClient.OnUnauthorized = OnUnauthorized;

            InitializeComponent();
        }
        protected override void OnStart()
        {
            //SecureStorage.Remove("usn");
            //SecureStorage.Remove("psw");
            SessionManager.Username = SecureStorage.GetAsync("usn").Result;
            SessionManager.Password = SecureStorage.GetAsync("psw").Result;

            if (string.IsNullOrEmpty(SessionManager.Username) || string.IsNullOrEmpty(SessionManager.Password))
            {
                IsLoginPage = true;
                MainPage = new Views.LoginView();
            }
            else
            {
                IsLoginPage = false;
                MainPage = new NavigationPage(new MainView());
            }
        }


        private void OnUnauthorized()
        {
            if (!IsLoginPage)
            {
                DataManager.MainThreadHandler.Abort();
                Device.InvokeOnMainThreadAsync(() => { _instance.MainPage = new LoginView(); });
                SecureStorage.Remove("usn");
                SecureStorage.Remove("psw");
            }
        }

        public static async Task LogOut()
        {
            DAL.DataManager.MainThreadHandler.Abort();
            DAL.SessionManager.Unauthorize();
            await Device.InvokeOnMainThreadAsync(() => { _instance.MainPage = new LoginView(); });
            SecureStorage.Remove("usn");
            SecureStorage.Remove("psw");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            
        }
    }
}
