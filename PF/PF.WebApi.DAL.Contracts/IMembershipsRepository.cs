using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IMembershipsRepository : IRepository<MembershipEntity>
    {
        Task<MembershipEntity> Get(long userId, long groupId);
        Task<IEnumerable<MembershipEntity>> GetAllOfUser(long userId);
        Task<IEnumerable<MembershipEntity>> GetAllOfGroup(long groupId);
    }
}