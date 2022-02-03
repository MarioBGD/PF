using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PF.App.Contracts;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using PF.App.Core.Models;

namespace PF.App.Core.ViewModels
{
    public class ExpensesViewModel
    {
        private readonly IUiThreadInvoker _uiThreadInvoker;
        
        public ExpensesViewModel(IExpensesServcie expensesServcie, IUiThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
            
            Expenses = new ObservableCollection<ExpensesGroupModel>();
            
            expensesServcie.GetTest(OnExpensesUpdate);
        }
        
        public ObservableCollection<ExpensesGroupModel> Expenses { get; }

        private Task OnExpensesUpdate(IEnumerable<Expense> expenses) =>
            _uiThreadInvoker.InvokeOnUIThreadAsync(() =>
            {
                ExpensesGroupsBuilder builder = new ExpensesGroupsBuilder();
                builder.Insert(expenses);
                var expensesViewModels = builder.GetGroupedExpenses();
                
                Expenses.Clear();
                expensesViewModels.ForEach(x => Expenses.Add(x));
            });
    }
}