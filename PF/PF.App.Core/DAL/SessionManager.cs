using System;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.DTO.Users;

namespace PF.App.Core.DAL
{
    public class SessionManager : ISessionManager
    {
        public string AuthToken { get; private set; }
        public long UserId { get; private set; }
        public Action OnUnauthorized { get; set; }

        public void OnAuthorized(string authToken, long userId)
        {
            AuthToken = authToken;
            UserId = userId;
        }

        public void Unauthorize()
        {
            AuthToken = null;
            UserId = 0;

            OnUnauthorized?.Invoke();
            OnUnauthorized = null;
        }
    }
}