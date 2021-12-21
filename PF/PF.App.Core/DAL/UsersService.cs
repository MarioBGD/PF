using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts;
using PF.DTO.Users;

namespace PF.App.Core.DAL
{
    public class UsersService : IUsersService
    {
        private readonly IApiCallsService _apiCallsService;
        private readonly IDataGetterService _dataGetterService;
        
        public UsersService(IApiCallsService apiCallsService, IDataGetterService dataGetterService)
        {
            _apiCallsService = apiCallsService;
            _dataGetterService = dataGetterService;
        }
        
        public Task<IEnumerable<UserDTO>> GetUsersDataAsync(List<long> usersId) => _dataGetterService.GetUsers(usersId);
    }
}