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
    public partial class ExpensesView : ContentPage
    {
        public ExpensesView()
        {
            InitializeComponent();
            BindingContext = new ExpensesViewModel();

            ExpensesListView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                ExpensesListView.SelectedItem = null;
            };
        }
    }
}