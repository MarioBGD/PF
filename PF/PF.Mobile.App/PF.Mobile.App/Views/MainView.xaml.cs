using PF.Mobile.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            Appearing += MainView_Appearing;
        }

        private void MainView_Appearing(object sender, EventArgs e)
        {
            Task.Run(async () =>
           {
               await SecureStorage.SetAsync("state", "0");
               AppBarViewModel.Instance.ShowPersonalInfo = true;
               AppBarViewModel.Instance.Header = "";
           });
        }
    }
}