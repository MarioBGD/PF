using System.Collections.Generic;
using System.Threading.Tasks;
using PF.DTO.Users;

namespace PF.WebApi.BLL.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsers(List<long> ids);
        public Task<UserDTO> UpdateUser(UserDTO user);
    }
}
