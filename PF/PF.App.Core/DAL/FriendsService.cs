using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL
{
    public class FriendsService : IFriendsService
    {
        private readonly IApiCallsService _apiCallsService;
        private readonly IDataGetterService _dataGetterService;
        private readonly IUsersService _usersService;
        
        public FriendsService(IApiCallsService apiCallsService, IDataGetterService dataGetterService, IUsersService usersService)
        {
            _apiCallsService = apiCallsService;
            _dataGetterService = dataGetterService;
            _usersService = usersService;
        }

        public void GetTest(IFriendsService.OnFriendsDataUpdate callback)
        {
            var friends = new List<Friend>
            {
                new Friend {Name = "Friend One", FriendId = 1, Status = Friend.FriendshipStatus.Invitation},
                new Friend {Name = "Friend Two", FriendId = 2, Status = Friend.FriendshipStatus.Invitation},
                new Friend {Name = "Friend Three", FriendId = 3, Status = Friend.FriendshipStatus.Invitation},
                new Friend {Name = "Friend Four", FriendId = 4, Status = Friend.FriendshipStatus.Accepted},
                new Friend {Name = "Friend Five", FriendId = 5, Status = Friend.FriendshipStatus.Accepted},
                new Friend {Name = "Friend Six", FriendId = 6, Status = Friend.FriendshipStatus.Accepted},
                new Friend {Name = "Friend Seven", FriendId = 7, Status = Friend.FriendshipStatus.Accepted}
            };

            callback.Invoke(friends);
        }
        
        public void GetFriends(IFriendsService.OnFriendsDataUpdate callback)
        {
            Task.Run(() => GetFriendsAsync(callback));
        }

        private async Task GetFriendsAsync(IFriendsService.OnFriendsDataUpdate callback)
        {
            var friendships = await _dataGetterService.GetFriendships();
            
            List<long> ids = friendships.Select(x => x.UserOneId).ToList();
            ids.AddRange(friendships.Select(x => x.UserTwoId));
            ids = ids.Distinct().ToList();

            var friendsUserDto = await _usersService.GetUsersDataAsync(ids);

            var friends = friendsUserDto.Select(x => new Friend
            {
                FriendId = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Login = x.Login,
                AvatarSrc = x.AvatarSrc,
                Status = (Friend.FriendshipStatus)(int)friendships.FirstOrDefault(y => y.UserOneId == x.Id || y.UserTwoId == x.Id)?.Status
            });

            callback.Invoke(friends);
        }

        public async Task<ServiceResult> AddFriendAsync(long friendId)
        {
            var res = await _apiCallsService.AddFriendById(friendId);
            return new ServiceResult(res.IsOk, res.Message);
        }

        public async Task<ServiceResult> RemoveFriendAsync(long friendId)
        {
            var res = await _apiCallsService.RemoveFriend(friendId);
            return new ServiceResult(res.IsOk, res.Message);
        }

        public async Task<ServiceResult> AddFriendByName(string name)
        {
            var res = await _apiCallsService.AddFriendByName(name);
            return new ServiceResult(res.IsOk, res.Message);
        }
    }
}