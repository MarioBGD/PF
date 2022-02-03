using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IExpensesRepository : IRepository<ExpenseEntity>
    {
        Task<IEnumerable<ExpenseEntity>> GetAllExpensesOfGroup(long groupId);
    }
}