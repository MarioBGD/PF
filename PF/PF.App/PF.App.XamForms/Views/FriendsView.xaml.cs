using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.App.Core.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsView
    {
        public FriendsView()
        {
            BindingContext = Locator.Current.GetService<FriendsViewModel>();
            InitializeComponent();
        }
    }
}