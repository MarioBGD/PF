using System;
using System.Threading.Tasks;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Contracts
{
    public interface ISessionManager
    {
        string AuthToken { get; }
        long UserId { get; }

        void OnAuthorized(string authToken, long userId);
        Action OnUnauthorized { get; set; }
        void Unauthorize();
    }
}