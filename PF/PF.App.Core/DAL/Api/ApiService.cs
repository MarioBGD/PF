using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Api
{
    public class ApiService : IApiCallsService
    {
        private readonly ISessionManager _sessionManager;
        
        public ApiService(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        
        public async Task<ApiResult<AuthTokenDTO>> Login(AuthDTO authDTO) =>
            await new ApiRequest<AuthTokenDTO>().Invoke("auth", _sessionManager.AuthToken, Method.Put, authDTO);

        public async Task<ApiResult<FriendshipDTO>> AddFriendById(long id) =>
            await new ApiRequest<FriendshipDTO>().Invoke($"friendships?id={id}", _sessionManager.AuthToken, Method.Post);

        public async Task<ApiResult<FriendshipDTO>> AddFriendByName(string name) =>
            await new ApiRequest<FriendshipDTO>().Invoke($"friendships?name={name}", _sessionManager.AuthToken, Method.Put);

        public async Task<ApiResult<string>> RemoveFriend(long id) =>
            await new ApiRequest<string>().Invoke($"friendships?id={id}", _sessionManager.AuthToken, Method.Delete);

        public async Task<ApiResult<string>> UpdateMyUser(UserDTO userDTO) =>
            await new ApiRequest<string>().Invoke($"users", _sessionManager.AuthToken, Method.Put, userDTO);

        public async Task<ApiResult<string>> Register(AuthDTO authDTO) =>
            await new ApiRequest<string>().Invoke("auth", _sessionManager.AuthToken, Method.Post, authDTO);

        public async Task<ApiResult<string>> CreateGroup(GroupDTO groupDTO) =>
            await new ApiRequest<string>().Invoke("groups", _sessionManager.AuthToken, Method.Post, groupDTO);

        public async Task<ApiResult<string>> DeleteGroup(long id) =>
            await new ApiRequest<string>().Invoke($"groups?Id={id}", _sessionManager.AuthToken, Method.Delete);

        public async Task<ApiResult<string>> AddPersonToGroup(long userId, long groupId) =>
            await new ApiRequest<string>().Invoke($"memberships?userId={userId}&groupId={groupId}", _sessionManager.AuthToken, Method.Post);

        public async Task<ApiResult<string>> DeletePersonFromGroup(long userId, long groupId) =>
            await new ApiRequest<string>().Invoke($"memberships?userId={userId}&groupId={groupId}", _sessionManager.AuthToken, Method.Delete);

        public async Task<ApiResult<string>> AddExpense(ExpenseDTO expense) =>
            await new ApiRequest<string>().Invoke($"expenses", _sessionManager.AuthToken, Method.Post, expense);
    }
}