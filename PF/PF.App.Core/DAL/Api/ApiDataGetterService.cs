using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;

namespace PF.App.Core.DAL.Api
{
    public class ApiDataGetterService : IDataGetterService
    {
        private readonly string _authToken;
        private readonly ISessionManager _sessionManager;
        
        public ApiDataGetterService(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _authToken = sessionManager.AuthToken;
        }
        
        
        public async Task<UserDTO> GetMyUser()
        {
            var res = await new ApiRequest<UserDTO[]>().Invoke($"users?ids={_sessionManager.UserId}&withImage={true}", _authToken, Method.Get);
            if (res.ResultContent != null && res.ResultContent.Length > 0)
                return res.ResultContent[0];
            return null;
        }

        public async Task<IEnumerable<FriendshipDTO>> GetFriendships(string status = "-1")
        {
            var res = await new ApiRequest<IEnumerable<FriendshipDTO>>().Invoke($"friendships?status={status}", _authToken, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<GroupDTO>> GetGroups(List<long> ids = null) 
        {
            var res = await new ApiRequest<IEnumerable<GroupDTO>>().Invoke("groups?ids=" + string.Join(',', ids), _authToken, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<MembershipDTO>> GetMemberships(long? groupId)
        {
            string req = "memberships";
            if (groupId.HasValue)
            {
                req += "?groupId=" + groupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<MembershipDTO>>().Invoke(req, _authToken, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(List<long> ids)
        {
            var res = await new ApiRequest<IEnumerable<UserDTO>>().Invoke($"users?ids={string.Join(',', ids)}&withImage={false}", _authToken, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenses(long? groupId)
        {
             string req = "expenses";
            if (groupId.HasValue)
            {
                req += "?groupId=" + groupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<ExpenseDTO>>().Invoke(req, _authToken, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<BalanceDTO>> GetBalances(BalanceRequestModel balanmceReq)
        {
            string req = $"balances?ids={string.Join(',', balanmceReq.UserIds)}";
            
            if (balanmceReq.GroupId.HasValue)
            {
                req += "&groupId=" + balanmceReq.GroupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<BalanceDTO>>().Invoke(req, _authToken, Method.Get);
            return res.ResultContent;
        }
    }
}