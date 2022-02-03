using System;
using System.Collections.Generic;
using System.Linq;
using PF.App.Core.DAL.Contracts.Models;
using PF.App.Core.ViewModels.Components;

namespace PF.App.Core.Models
{
    public class ExpensesGroupsBuilder
    {
        private readonly Dictionary<TimeCategory, Expense> _groups;
        
        public ExpensesGroupsBuilder()
        {
            _groups = new Dictionary<TimeCategory, Expense>();
        }

        public void Insert(IEnumerable<Expense> expenses)
        {
            expenses = expenses.OrderByDescending(x => x.CreatedDate);
            
            foreach (var expense in expenses)
            {
                var timeCategory = GetTimeCategory(expense.CreatedDate);
                _groups.Add(timeCategory, expense);
            }
        }

        public List<ExpensesGroupModel> GetGroupedExpenses()
        {
            var grouped = new List<ExpensesGroupModel>();

            var groupedExpenses = _groups.GroupBy(x => x.Key.Category.ToString() + x.Key.Number);

            foreach (var groupedExpense in groupedExpenses)
            {
                var expenses = groupedExpense.Select(x => x.Value).AsEnumerable();
                var expensesViewModels = expenses.Select(x => new ExpenseComponentViewModel(x));
                var groupModel = new ExpensesGroupModel(groupedExpense.Key, expensesViewModels);
                grouped.Add(groupModel);
            }
            
            return grouped;
        }

        private TimeCategory GetTimeCategory(DateTime date)
        {
            date = date.Date;
            var now = DateTime.Now.Date;

            if (now == date)
            {
                return new TimeCategory(TimeCategory.Categories.Today);
            } 
            
            if (now - TimeSpan.FromDays(1) == date)
            {
                return new TimeCategory(TimeCategory.Categories.Yesterday);
            }

            var currentMonth = now.Year * 12 + now.Month;
            var dateMonth = date.Year * 12 + date.Month;
            var monthsDifference = currentMonth - dateMonth;

            if (monthsDifference == 0)
            {
                return new TimeCategory(TimeCategory.Categories.ThisMonth);
            }

            return new TimeCategory(TimeCategory.Categories.XMonthsAho, monthsDifference);
        }
    }
}