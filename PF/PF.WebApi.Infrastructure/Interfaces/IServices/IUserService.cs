using PF.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Infrastructure.Interfaces.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsers(List<long> ids);
        public Task<UserDTO> UpdateUser(UserDTO user);
    }
}
