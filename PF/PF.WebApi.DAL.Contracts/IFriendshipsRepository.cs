using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IFriendshipsRepository : IRepository<FriendshipEntity>
    {
        Task<IEnumerable<FriendshipEntity>> GetAllFriendsOfUser(long userId);

        Task<FriendshipEntity> GetFriendship(long userId1, long userId2);

        Task<int> DeleteFriendship(long userId1, long userId2);
    }
}