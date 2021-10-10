using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PF.Mobile.App.DAL.Api
{
     public class ApiClient : ISmartClient
    {
        public delegate void OnUnauthorizedDel();
        public static OnUnauthorizedDel OnUnauthorized;

        //public StandardSocketsHttpHandler SocketsHandler { get; private set; }
        //public HttpClient Client;

        //public ApiClient()
        //{
        //    SocketsHandler = new StandardSocketsHttpHandler
        //    {
        //        PooledConnectionIdleTimeout = TimeSpan.FromSeconds(100),
        //        PooledConnectionLifetime = TimeSpan.FromSeconds(60),
        //        MaxConnectionsPerServer = 5,
                
        //    };
        //    Client = new HttpClient();
        //}

        public async static Task<ApiResult<AuthTokenDTO>> Login(AuthDTO authDTO) =>
            await new ApiRequest<AuthTokenDTO>().Invoke("auth", Method.Put, authDTO);

        public async static Task<ApiResult<FriendshipDTO>> AddFriendById(long id) =>
            await new ApiRequest<FriendshipDTO>().Invoke($"friendships?id={id}", Method.Post);

         public async static Task<ApiResult<FriendshipDTO>> AddFriendByName(string name) =>
            await new ApiRequest<FriendshipDTO>().Invoke($"friendships?name={name}", Method.Put);

        public async static Task<ApiResult<string>> RemoveFriend(long id) =>
            await new ApiRequest<string>().Invoke($"friendships?id={id}", Method.Delete);

        public async static Task<ApiResult<string>> Register(AuthDTO authDTO) =>
            await new ApiRequest<string>().Invoke("auth", Method.Post, authDTO);

        public async static Task<ApiResult<string>> CreateGroup(GroupDTO groupDTO) =>
            await new ApiRequest<string>().Invoke("groups", Method.Post, groupDTO);

        public async static Task<ApiResult<string>> DeleteGroup(long id) =>
            await new ApiRequest<string>().Invoke($"groups?Id={id}", Method.Delete);

        public async static Task<ApiResult<string>> AddPersonToGroup(long userId, long groupId) =>
            await new ApiRequest<string>().Invoke($"memberships?userId={userId}&groupId={groupId}", Method.Post);

        public async static Task<ApiResult<string>> DeletePersonFromGroup(long userId, long groupId) =>
            await new ApiRequest<string>().Invoke($"memberships?userId={userId}&groupId={groupId}", Method.Delete);

        public async static Task<ApiResult<string>> AddExpense(ExpenseDTO expense) =>
            await new ApiRequest<string>().Invoke($"expenses", Method.Post, expense);





        public async Task<UserDTO> GetMyUser()
        {
            var res = await new ApiRequest<UserDTO[]>().Invoke($"users?ids={SessionManager.UserId}&withImage={true}", Method.Get);
            if (res.ResultContent != null && res.ResultContent.Length > 0)
                return res.ResultContent[0];
            return null;
        }

        public async Task<IEnumerable<FriendshipDTO>> GetFriendships(string status = "-1")
        {
            var res = await new ApiRequest<IEnumerable<FriendshipDTO>>().Invoke($"friendships?status={status}", Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<GroupDTO>> GetGroups(List<long> ids = null) 
        {
            var res = await new ApiRequest<IEnumerable<GroupDTO>>().Invoke("groups?ids=" + string.Join(',', ids), Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<MembershipDTO>> GetMemberships(long? groupId)
        {
            string req = "memberships";
            if (groupId.HasValue)
            {
                req += "?groupId=" + groupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<MembershipDTO>>().Invoke(req, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(List<long> ids)
        {

            var res = await new ApiRequest<IEnumerable<UserDTO>>().Invoke($"users?ids={string.Join(',', ids)}&withImage={false}", Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenses(long? groupId)
        {
             string req = "expenses";
            if (groupId.HasValue)
            {
                req += "?groupId=" + groupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<ExpenseDTO>>().Invoke(req, Method.Get);
            return res.ResultContent;
        }

        public async Task<IEnumerable<BalanceDTO>> GetBalances(BalanceRequestModel balanmceReq)
        {
            string req = $"balances?ids={string.Join(',', balanmceReq.UserIds)}";
            
            if (balanmceReq.GroupId.HasValue)
            {
                req += "&groupId=" + balanmceReq.GroupId.Value.ToString();
            }

            var res = await new ApiRequest<IEnumerable<BalanceDTO>>().Invoke(req, Method.Get);
            return res.ResultContent;
        }
    }
}
