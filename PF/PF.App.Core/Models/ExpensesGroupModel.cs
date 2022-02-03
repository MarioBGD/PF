using System.Collections.Generic;
using System.Collections.ObjectModel;
using PF.App.Core.ViewModels.Components;

namespace PF.App.Core.Models
{
    public class ExpensesGroupModel : ObservableCollection<ExpenseComponentViewModel>
    {
        public ExpensesGroupModel(string name)
        {
            GroupName = name;
        }
        
        public ExpensesGroupModel(string name, IEnumerable<ExpenseComponentViewModel> source) : base(source)
        {
            GroupName = name;
        }
        
        public string GroupName { get; }
    }
}