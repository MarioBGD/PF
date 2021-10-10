using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PF.Mobile.App.DAL
{
    public interface ISmartClient
    {
        public Task<UserDTO> GetMyUser();
        public Task<IEnumerable<FriendshipDTO>> GetFriendships(string status = "-1");
        public Task<IEnumerable<GroupDTO>> GetGroups(List<long> ids = null);
        public Task<IEnumerable<MembershipDTO>> GetMemberships(long? groupId);
        public Task<IEnumerable<UserDTO>> GetUsers(List<long> ids);
        public Task<IEnumerable<ExpenseDTO>> GetExpenses(long? groupId);
        public Task<IEnumerable<BalanceDTO>> GetBalances(BalanceRequestModel balanceReq);
    }
}
