using PF.DTO.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL.Contracts
{
    public interface IGroupService
    {
        public Task<IEnumerable<GroupDTO>> GetAllGroupsOfUser(long userId);
        public Task<IEnumerable<GroupDTO>> GetSelectedGroupsOfUser(long userId, List<long> groups);
        public Task<GroupDTO> CreateGroup(GroupDTO group, long ownerUserId);
    }
}
