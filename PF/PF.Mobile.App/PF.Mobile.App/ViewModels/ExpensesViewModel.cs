using PF.DTO.Expenses;
using PF.Mobile.App.DAL;
using PF.Mobile.App.Models;
using PF.Mobile.App.ViewModels.Items;
using PF.Mobile.App.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
    public class ExpensesViewModel : BaseViewModel
    {
        public ObservableCollection<ExpenseItemViewModel> Expenses { get; set; }

        public Command AddExpenseCommand { get; }

        public ExpensesViewModel()
        {
            AddExpenseCommand = new Command(async () => await OnAddExpenseClick(), () => !IsBusy);
            Expenses = new ObservableCollection<ExpenseItemViewModel>();

            DataManager.GetData(DataManager.DataType.Expenses, OnExpensesDataUpdate, App.CurrentGroupId);
        }

        private void OnExpensesDataUpdate(object data)
        {
            if (data == null)
            {
                return;
            }

            IEnumerable<ExpenseDTO> expenses = (IEnumerable<ExpenseDTO>)data;
            expenses = expenses.OrderByDescending(x => x.CreatedDate).AsEnumerable();

            Device.InvokeOnMainThreadAsync(() =>
            {
                Expenses.Clear();

                var timeConverter = new TimeDifferenceToTimeIntervalConverter(DateTime.Now);
                int index = 0;
                string lastTimeSpan = "";

                foreach (var expense in expenses)
                {
                    var exp = new ExpenseItemViewModel(expense);

                    string timeSpan = timeConverter.ToTimeSinceString(exp.CreatedDate);

                    if (timeSpan != lastTimeSpan)
                    {
                        exp.ShowHeader = true;
                        exp.HeaderText = timeSpan;
                        lastTimeSpan = timeSpan;
                    }

                    Expenses.Add(exp);
                }
            });
        }

        private async Task OnAddExpenseClick()
        {
            IsBusy = true;

            var view = new Views.Popups.ExpensePopupView(new ExpenseDTO());
            ((ExpensePopupViewModel)view.BindingContext).OnPoped = OnAddedNewExpense;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);

            IsBusy = false;
        }

        private void OnAddedNewExpense(object res)
        {
            DataManager.GetData(DataManager.DataType.Expenses, OnExpensesDataUpdate, App.CurrentGroupId);
        }
    }
}
