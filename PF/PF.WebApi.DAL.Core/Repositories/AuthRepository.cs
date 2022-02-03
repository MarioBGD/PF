using Dapper;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class AuthRepository : Repository<AuthEntity>, IAuthRepository
    {
        public AuthRepository(IUnitOfWork uow) : base (uow.Connection, uow.Transaction, "Auth")
        {

        }

        public async Task<AuthEntity> GetUserByLogin(string login)
        {
            var result = await Connection.QuerySingleOrDefaultAsync<AuthEntity>($"SELECT * FROM {TableName} WHERE Login='{login}'", null, Transaction);
            return result;
        }

    }
}
