using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.ViewModels.Components
{
    public class ExpenseComponentViewModel
    {
        public ExpenseComponentViewModel(Expense expense)
        {
            Name = expense.Title;
        }
        
        public string Name { get; }
    }
}