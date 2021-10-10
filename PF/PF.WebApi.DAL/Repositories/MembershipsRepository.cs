using Dapper;
using PF.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Repositories
{
    public class MembershipsRepository : Repository<MembershipEntity>
    {
        public MembershipsRepository(IUnitOfWork uow) : base (uow.Connection, uow.Transaction, "Memberships")
        {

        }

        public async Task<MembershipEntity> Get(long userId, long groupId)
        {
            string query = $"SELECT * FROM {TableName} WHERE UserId = {userId} AND GroupId = {groupId}";
            return await Connection.QueryFirstOrDefaultAsync<MembershipEntity>(query, null, Transaction);
        }

        public async Task<IEnumerable<MembershipEntity>> GetAllOfUser(long userId)
        {
            string query = $"SELECT * FROM {TableName} WHERE UserId = {userId}";
            return await Connection.QueryAsync<MembershipEntity>(query, null, Transaction);
        }

         public async Task<IEnumerable<MembershipEntity>> GetAllOfGroup(long groupId)
        {
            string query = $"SELECT * FROM {TableName} WHERE GroupId = {groupId}";
            return await Connection.QueryAsync<MembershipEntity>(query, null, Transaction);
        }
    }
}
