using Microsoft.AspNetCore.Mvc;
using PF.DTO.Groups;
using PF.WebApi.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.Common.Extensions;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> Get(string ids = null)
        {
            long userId = await SessionManager.Authorize(HttpContext);

            if (string.IsNullOrEmpty(ids))
            {
                var groups = await _groupService.GetAllGroupsOfUser(userId);
                return Ok(groups);
            }
            else
            { //OLD

                List<long> numberIds = ids.ToLongList();
                IEnumerable<GroupDTO> groups = null;

                if (numberIds.Count == 0)
                {
                    groups = await _groupService.GetAllGroupsOfUser(userId);
                }
                else
                {
                    groups = await _groupService.GetSelectedGroupsOfUser(userId, numberIds);
                }

                return Ok(groups);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> Add([FromBody] GroupDTO group)
        {
            long userId = await SessionManager.Authorize(HttpContext);

            group.DefaultCurrencyId = 3;
            group.Currencies = new List<GroupCurrencyDTO>
            {
                new GroupCurrencyDTO
                {
                    CurrencyId = 1,
                    Worth = 4.6m
                },
                new GroupCurrencyDTO
                {
                    CurrencyId = 4,
                    Worth = 0.14m
                },
            };

            var result = await _groupService.CreateGroup(group, userId);

            return Ok(result);
        }
    }
}
