using PF.App.Contracts.Navigation;
using PF.App.XamForms.Navigation;
using Splat;
using System;
using PF.App.Contracts;
using PF.App.Contracts.Startup;
using PF.App.Core.ViewModels.Startup;
using PF.App.XamForms.Views.Startup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            
            Locator.Current.GetService<IFormsNavigationProvider>()!.Navigation = MainPage.Navigation;
            Locator.Current.GetService<IStartupCoordinator>()!.Start();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
