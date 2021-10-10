using Dapper;
using PF.DTO.Users;
using PF.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Repositories
{
    public class UsersRepository : Repository<UserEntity>
    {
        public UsersRepository(IUnitOfWork ouw) : base (ouw.Connection, ouw.Transaction, "Users")
        {

        }

        public async Task<UserDTO> GetByLogin(string login)
        {
            var result = await Connection.QuerySingleOrDefaultAsync<UserDTO>($"SELECT * FROM {TableName} WHERE Login='{login}'", null, Transaction);
            return result;
        }
    }
}
