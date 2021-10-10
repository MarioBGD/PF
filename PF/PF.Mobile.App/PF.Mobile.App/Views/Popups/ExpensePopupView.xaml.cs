using PF.DTO.Expenses;
using PF.Mobile.App.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ExpensePopupViewModel ViewModel;

        public ExpensePopupView(ExpenseDTO expense = null)
        {
            InitializeComponent();
            ViewModel = new ExpensePopupViewModel(expense);
            BindingContext = ViewModel;
        }

        private async void AmountUnfocusedEvent(object sender, FocusEventArgs e)
        {
            await ViewModel.OnValueEntered();
        }

       
    }
}