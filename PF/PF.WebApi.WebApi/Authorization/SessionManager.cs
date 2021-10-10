using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.WebApi.WebApi.Authorization
{
    public static class SessionManager
    {
        [Obsolete]
        private static long? CurrentUserId { get; set; }
        [Obsolete]
        private static readonly bool UseSingleUserAuth = true;

        private static object Locker;
        private static PF.Common.Crypto.IHasher TokenHasher;
        private static Dictionary<string, Session> SessionsHandler;

        
        public static void Initialize()
        {
            TokenHasher = new PF.Common.Crypto.SHA256();
            SessionsHandler = new Dictionary<string, Session>();
            Locker = new object();
        }


        /// <returns>Auth token</returns>
        public static async Task<string> BeginNewSession(long userId)
        {
            DateTime now;
            string token;

            lock (Locker)
            {
                do
                {
                    now = DateTime.Now;
                    token = TokenHasher.HashToBase64String(now.Ticks.ToString());
                }
                while (SessionsHandler.ContainsKey(token));

                Session session = new Session
                {
                    UserId = userId,
                    Token = token
                };
                SessionsHandler.Add(token, session);
                CurrentUserId = userId;
            }

            return token;
        }

        public static async Task<long> Authorize(HttpContext httpContext)
        {
            try
            {
                if (UseSingleUserAuth)
                {
                    return CurrentUserId.Value;
                }

                Microsoft.Extensions.Primitives.StringValues token;
                if (!httpContext.Request.Headers.TryGetValue("Authorization", out token))
                {
                    throw new UnauthorizedAccessException("Token not sended");
                }

                Session session;

                lock (Locker)
                {
                    if (!SessionsHandler.TryGetValue(token, out session))
                    {
                        throw new UnauthorizedAccessException("Cannot find token");
                    }
                }

                return session.UserId;
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException(e.Message);
            }
        }

        public static async Task Unauthorize(string token)
        {
            lock (Locker)
            {
                if (SessionsHandler.ContainsKey(token))
                {
                    SessionsHandler.Remove(token);
                }
            }
        }
    }
}
