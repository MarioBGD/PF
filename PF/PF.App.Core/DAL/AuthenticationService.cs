using System;
using System.Net;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.DTO.Users;

namespace PF.App.Core.DAL
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApiCallsService _apiCallsService;
        private readonly ISessionManager _sessionManager;
        
        public AuthenticationService(IApiCallsService apiCallsService, ISessionManager sessionManager)
        {
            _apiCallsService = apiCallsService;
            _sessionManager = sessionManager;
        }
        
        public async Task<AuthTokenDTO> LoginAsync(string userName, string hashedPassword)
        {
            AuthDTO dto = new AuthDTO()
            {
                Login = userName,
                HashedPassword = hashedPassword
            };

            var res = await _apiCallsService.Login(dto);
            var authToken = res.ResultContent;

            if (authToken?.Success == true)
            {
                _sessionManager.OnAuthorized(authToken.Token, authToken.UserId);
            }

            return authToken;
        }

        public async Task<AuthTokenDTO> RegisterAsync(string userName, string hashedPassword, string firstName, string  lastName)
        {
            AuthDTO dto = new AuthDTO()
            {
                Login = userName,
                HashedPassword = hashedPassword,
                Name = firstName,
                LastName = lastName
            };

            var result = await _apiCallsService.Register(dto);

            return new AuthTokenDTO()
            {
                Message = result.Message,
                Success = result.StatusCode == HttpStatusCode.OK,
                Token = result.ResultContent,
            };
        }
    }
}