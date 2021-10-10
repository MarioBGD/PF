using PF.DTO.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Infrastructure.Interfaces.IServices
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
