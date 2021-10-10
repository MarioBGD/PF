using PF.Mobile.App.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePeopleItemView : ContentView
    {
        
        public ExpensePeopleItemView()
        {
            InitializeComponent();
            //BindingContext = new ExpensePeopleItemViewModel();
        }
    }
}