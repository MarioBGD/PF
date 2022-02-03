using PF.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL.Contracts
{
    public interface IFriendshipService
    {
        public Task<IEnumerable<FriendshipDTO>> GetFriendsOfUser(long userId);
        public Task<FriendshipDTO> AddFriendById(long senderUserId, long reciverUserId);
        public Task<FriendshipDTO> AddFriendByName(long senderUserId, string reciverUsername);
        public Task<bool> RemoveFriendship(long userOne, long userTwo);
    }
}
