using Microsoft.AspNetCore.Mvc;
using PF.DTO.Users;
using PF.WebApi.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IAccountService _authService;

        public AuthController(IAccountService authService)
        {
            _authService = authService;
        }


        [HttpPut]
        public async Task<ActionResult<AuthTokenDTO>> Login([FromBody] AuthDTO authData)
        {
            AuthTokenDTO token = await _authService.Login(authData);

            if (token == null || !token.Success)
            {
                return Unauthorized(token?.Message);
            }

            token.Token = await SessionManager.BeginNewSession(token.UserId);

            return Ok(token);
        }

        [HttpPost]
        public async Task<ActionResult<AuthTokenDTO>> Register([FromBody] AuthDTO authData)
        {
            AuthTokenDTO token = await _authService.Register(authData);

            if (token == null || !token.Success)
            {
                return Unauthorized(token?.Message);
            }

            token.Token = await SessionManager.BeginNewSession(token.UserId);

            return Ok(token);
        }
    }
}
