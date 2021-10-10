using PF.Mobile.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppBarView : ContentView
    {
        public AppBarView()
        {
            InitializeComponent();
            BindingContext = AppBarViewModel.Instance;
        }
    }
}