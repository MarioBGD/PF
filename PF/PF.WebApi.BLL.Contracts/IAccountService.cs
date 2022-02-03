using PF.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL.Contracts
{
    public interface IAccountService
    {
        public Task<AuthTokenDTO> Login(AuthDTO authData);

        public Task<AuthTokenDTO> Register(AuthDTO authData);
    }
}
