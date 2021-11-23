using System;
using System.Threading.Tasks;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthTokenDTO> LoginAsync(string userName, string hashedPassword);
        Task<AuthTokenDTO> RegisterAsync(string userName, string hashedPassword, string firstName, string  lastName);
    }
}