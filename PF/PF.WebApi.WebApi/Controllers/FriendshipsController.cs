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
    [Route("api/friendships")]
    public class FriendshipsController : ControllerBase
    {
        private IFriendshipService _friendshipService;

        public FriendshipsController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendshipDTO>>> Get()
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            IEnumerable<FriendshipDTO> friendships = await _friendshipService.GetFriendsOfUser(sourceUserId);
            return Ok(friendships);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<FriendshipDTO>>> AddByName(string name)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            FriendshipDTO friendship = await _friendshipService.AddFriendByName(sourceUserId, name);

            if (friendship == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(friendship);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendshipDTO>>> AddById(long id)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            FriendshipDTO friendship = await _friendshipService.AddFriendById(sourceUserId, id);

            if (friendship == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(friendship);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<FriendshipDTO>>> Remove(long id)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            bool removed = await _friendshipService.RemoveFriendship(sourceUserId, id);

            return Ok(removed);
        }
    }
}
