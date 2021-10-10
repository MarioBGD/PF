using Microsoft.AspNetCore.Mvc;
using PF.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.Common.Extensions;
using PF.WebApi.Infrastructure.Interfaces.IServices;
using PF.WebApi.WebApi.Authorization;

namespace PF.WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get(string ids, bool withImage = false)
        {
            await SessionManager.Authorize(HttpContext);

            List<long> numberIds = ids.ToLongList();

            IEnumerable<UserDTO> users = await _userService.GetUsers(numberIds);

            return Ok(users);
        }
    }
}
