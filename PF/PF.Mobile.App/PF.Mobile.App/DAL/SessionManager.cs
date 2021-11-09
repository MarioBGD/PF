using PF.DTO.Users;
using PF.Mobile.App.DAL.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PF.Mobile.App.DAL
{
     public static class SessionManager
    {
        public enum SessionState { Offline, Authorized, Unauthorized, Authorizing }

        public static string AuthToken { get; private set; }
        public static long UserId { get; private set; }
        public static UserDTO UserDTO { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static SessionState State { get; private set; } = SessionState.Unauthorized;
        public static event EventHandler OnSessionStateChanged;

      
        public static async Task<SessionState> Authorize(string login, string hashedPassword)
        {
            State = SessionState.Authorizing;

            AuthDTO authDTO = new AuthDTO
            {
                Login = login,
                HashedPassword = hashedPassword
            };

            var resp = await ApiClient.Login(authDTO);

            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AuthToken = resp.ResultContent.Token;
                UserId = resp.ResultContent.UserId;
                State = SessionState.Authorized;
                return SessionState.Authorized;
            }

            return SessionState.Unauthorized;
        }

        public static void Unauthorize()
        {
            State = SessionState.Unauthorized;
            AuthToken = null;
            UserId = 0;
        }
    }
}
