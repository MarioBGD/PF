using Dapper;
using PF.WebApi.DAL.Entities;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class UsersRepository : Repository<UserEntity>, IUsersRepository
    {
        public UsersRepository(IUnitOfWork ouw) : base (ouw.Connection, ouw.Transaction, "Users")
        {

        }

        public async Task<UserEntity> GetByLogin(string login)
        {
            var result = await Connection.QuerySingleOrDefaultAsync<UserEntity>($"SELECT * FROM {TableName} WHERE Login='{login}'", null, Transaction);
            return result;
        }
    }
}
