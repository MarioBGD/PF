using Dapper;
using PF.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Repositories
{
    public class FriendshipsRepository : Repository<FriendshipEntity>
    {
        public FriendshipsRepository(IUnitOfWork uow) : base (uow.Connection, uow.Transaction, "Friendships")
        {

        }

        public async Task<IEnumerable<FriendshipEntity>> GetAllFriendsOfUser(long userId)
        {
            string query = $"SELECT * FROM {TableName} WHERE {userId} IN (UserOneId, UserTwoId)";
            return await Connection.QueryAsync<FriendshipEntity>(query, null, Transaction);
        }

        public async Task<FriendshipEntity> GetFriendship(long userId1, long userId2)
        {
            string query = $"SELECT * FROM {TableName} WHERE UserOneId IN ({userId1}, {userId2}) AND UserTwoId IN ({userId1}, {userId2})";
            return await Connection.QueryFirstOrDefaultAsync<FriendshipEntity>(query, null, Transaction);
        }

        public async Task<int> DeleteFriendship(long userId1, long userId2)
        {
            string query = $"DELETE FROM {TableName} WHERE UserOneId IN ({userId1}, {userId2}) AND UserTwoId IN ({userId1}, {userId2})";
            return await Connection.ExecuteAsync(query, null, Transaction);
        }
    }
}
