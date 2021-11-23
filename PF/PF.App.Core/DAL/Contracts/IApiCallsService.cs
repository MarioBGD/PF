using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;
using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Contracts
{
    public interface IApiCallsService
    {
        Task<ApiResult<AuthTokenDTO>> Login(AuthDTO authDto);

        Task<ApiResult<FriendshipDTO>> AddFriendById(long id);

        Task<ApiResult<FriendshipDTO>> AddFriendByName(string name);

         Task<ApiResult<string>> RemoveFriend(long id);

        Task<ApiResult<string>> UpdateMyUser(UserDTO userDto);

        Task<ApiResult<string>> Register(AuthDTO authDto);

        Task<ApiResult<string>> CreateGroup(GroupDTO groupDto);

        Task<ApiResult<string>> DeleteGroup(long id);

        Task<ApiResult<string>> AddPersonToGroup(long userId, long groupId);

        Task<ApiResult<string>> DeletePersonFromGroup(long userId, long groupId);

        Task<ApiResult<string>> AddExpense(ExpenseDTO expense);
    }
}