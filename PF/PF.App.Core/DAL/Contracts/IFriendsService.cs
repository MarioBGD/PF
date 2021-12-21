using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IFriendsService
    {
        delegate void OnFriendsDataUpdate(IEnumerable<Friend> friends);

        void GetFriends(OnFriendsDataUpdate callback);
        Task<ServiceResult> AddFriendAsync(long friendId);
        Task<ServiceResult> AddFriendByName(string name);
        Task<ServiceResult> RemoveFriendAsync(long friendId);
    }
}