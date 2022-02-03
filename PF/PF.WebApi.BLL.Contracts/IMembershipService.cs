using System.Collections.Generic;
using System.Threading.Tasks;
using PF.DTO.Groups;

namespace PF.WebApi.BLL.Contracts
{
    public interface IMembershipService
    {
        public Task<bool> AddMember(long userId, long groupId);
        public Task<bool> RemoveMember(long userId, long groupId);
        public Task<bool> UpdateMemberStatus(long userId, long groupId);
        public Task<IEnumerable<MembershipDTO>> GetAllMembershipsOfUser(long userId);
        public Task<IEnumerable<MembershipDTO>> GetAllMembershipsOfGroup(long groupId);
    }
}
