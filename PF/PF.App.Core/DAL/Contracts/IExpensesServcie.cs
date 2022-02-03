using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IExpensesServcie
    {
        delegate Task OnExpensesDataUpdate(IEnumerable<Expense> expenses);

        void GetExpenses(OnExpensesDataUpdate callback, CancellationToken cancellationToken);
        void GetTest(OnExpensesDataUpdate callback);
    }
}