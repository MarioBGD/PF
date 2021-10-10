using Dapper;
using PF.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Repositories
{
    public class GroupCurrenciesRepository : Repository<GroupCurrencyEntity>
    {
        public GroupCurrenciesRepository(IUnitOfWork uow) : base(uow.Connection, uow.Transaction, "GroupCurrencies")
        {

        }

        public async Task<IEnumerable<GroupCurrencyEntity>> GetCurrenciesOfGroup(long groupId)
        {
            string query = $"SELECT * FROM {TableName} WHERE GroupId = {groupId.ToString()}";
            return await Connection.QueryAsync<GroupCurrencyEntity>(query, null, Transaction);
        }
    }
}
