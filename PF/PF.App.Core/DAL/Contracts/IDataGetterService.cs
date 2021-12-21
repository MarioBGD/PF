using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;
using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Contracts
{
    public interface IDataGetterService
    {
        Task<UserDTO> GetMyUser();
        
        Task<IEnumerable<FriendshipDTO>> GetFriendships(string status = "-1");
        
        Task<IEnumerable<GroupDTO>> GetGroups(List<long> ids = null);

        Task<IEnumerable<MembershipDTO>> GetMemberships(long? groupId);

        Task<IEnumerable<UserDTO>> GetUsers(List<long> ids);

        Task<IEnumerable<ExpenseDTO>> GetExpenses(long? groupId);

        Task<IEnumerable<BalanceDTO>> GetBalances(BalanceRequestModel balanceReq);
    }
}