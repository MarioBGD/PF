using PF.DTO.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Infrastructure.Interfaces.IServices
{
    public interface IExpenseService
    {
        public Task<IEnumerable<ExpenseDTO>> GetAllExpensesOfGroup(long sourceUserId, long groupId);
        public Task<IEnumerable<ExpenseDTO>> GetExpenses(IEnumerable<long> ids);
        public Task<bool> AddExpense(ExpenseDTO expense);
        public Task<bool> UpdateExpense(ExpenseDTO expense);
        public Task<bool> DeleteExpense(long expenseId);
    }
}
