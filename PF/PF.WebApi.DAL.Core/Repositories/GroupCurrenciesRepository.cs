using Dapper;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class GroupCurrenciesRepository : Repository<GroupCurrencyEntity>, IGroupCurrenciesRepository
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
