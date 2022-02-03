using Dapper;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class ExpensesRepository : Repository<ExpenseEntity>, IExpensesRepository
    {
        public ExpensesRepository(IUnitOfWork uow) : base(uow.Connection, uow.Transaction, "Expenses")
        {

        }

        public async Task<IEnumerable<ExpenseEntity>> GetAllExpensesOfGroup(long groupId)
        {
            string query = $"SELECT * FROM {TableName} WHERE GroupId = {groupId.ToString()}";
            return await Connection.QueryAsync<ExpenseEntity>(query, null, Transaction);
        }
    }
}
