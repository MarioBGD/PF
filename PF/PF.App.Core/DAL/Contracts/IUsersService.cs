using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Contracts
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDTO>> GetUsersDataAsync(List<long> usersId);
        Task<User> GetMyUserDataAsync();

        // delegate void OnUsersDataUpdate(IEnumerable<UserDTO> friends);
        //
        // void GetFriends(OnFriendsDataUpdate callback);
        // Task<ServiceResult> AddFriendAsync(long friendId);
        // Task<ServiceResult> RemoveFriendAsync(long friendId);
    }
}