using Microsoft.AspNetCore.Mvc;
using PF.DTO.Groups;
using PF.WebApi.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.WebApi.Controllers
{
     [ApiController]
    [Route("api/memberships")]
    public class MembershipsController : ControllerBase
    {
        private IMembershipService _membershipService;

        public MembershipsController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> Get(Nullable<long> groupId)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            IEnumerable<MembershipDTO> memberships = null;

            if (groupId.HasValue)
            {
                memberships = await _membershipService.GetAllMembershipsOfGroup(groupId.Value);
            }
            else
            {
                memberships = await _membershipService.GetAllMembershipsOfUser(sourceUserId);
            }

            return Ok(memberships);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> Add(long userId, long groupId)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            bool result = await _membershipService.AddMember(userId, groupId);

            return result ? Ok() : Problem();
        }
    }
}
